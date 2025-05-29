using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Dapper;
using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private readonly object locker = new();
    [UsedImplicitly] private HttpClient httpClient;
    private readonly SqliteConnection connection;
    private readonly AppSetting appSetting;
    private readonly ILogger<PoetryService> logger;
    private const string worksRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private const string collectionListRoute = "/api/v1/collections";
    private List<CollectionInfo> collectionCache = [];
    private readonly List<WorkInfo> workCache = [];

    public PoetryService(ILogger<PoetryService> logger, AppSetting appSetting)
    {
        this.logger = logger;
        this.appSetting = appSetting;

        if (!File.Exists(AppSetting.SQLiteFilePath))
        {
            File.Copy(AppSetting.SQLiteFileName, AppSetting.SQLiteFilePath);
            logger.LogInformation($"copy init database from {AppSetting.SQLiteFileName} to {AppSetting.SQLiteFilePath}");
        }
        connection = new SqliteConnection($"DataSource={AppSetting.SQLiteFilePath};Cache=Shared");
        logger.LogInformation("Current data root path: {}", AppSetting.ConfigRootPath);

        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://home.freemanke.com:60011") };
    }

    private List<WorkInfo> GetWorks()
    {
        if (workCache.Count != 0) return workCache;
        try
        {
            lock (locker)
            {
                var items = connection.Query(
                        "select id as Id, title as Title, author as Author, content as Content, dynasty as Dynasty from works")
                    .ToWorks();
                workCache.AddRange(items);
                logger.LogInformation($"{nameof(GetWorks)} {items.Count}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return workCache;
    }

    public List<WorkListItemInfo> GetWorkList()
    {
        var items = new List<WorkListItemInfo>();
        try
        {
            lock (locker)
            {
                var works = GetWorks();
                items = works.Select(a => new WorkListItemInfo
                {
                    Id = a.Id,
                    Title = a.Title ?? string.Empty,
                    Author = a.Author ?? string.Empty,
                    Content = a.Content ?? string.Empty,
                    Dynasty = a.Dynasty ?? string.Empty
                }).ToList();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        logger.LogInformation($"{nameof(GetWorkList)} {items.Count}");
        return items;
    }

    public List<WorkListItemInfo> GetWorkList(int collectionId)
    {
        var items = new List<WorkListItemInfo>();
        try
        {
            lock (locker)
            {
                var workList = GetWorkList();
                var workIds = connection.Query(
                        $"select work_id as WorkId from collection_works where collection_id={collectionId}")
                    .ToCollectionWorks().Select(a => a.WorkId).ToList();
                items = workList.Where(a => workIds.Contains(a.Id)).ToList();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        logger.LogInformation($"{nameof(GetWorkList)} {items.Count}");
        return items;
    }

    public List<CollectionInfo> GetCollectionList()
    {
        try
        {
            lock (locker)
            {
                var items = connection.Query("select id as Id, name as Name from collections")
                    .ToCollections();

                var infos = items.Select(a => new CollectionInfo
                {
                    Id = a.Id, Name = a.Name
                }).ToList();
                return infos;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return [];
    }

    public void Favorite(int id, bool isFavorite)
    {
        switch (isFavorite)
        {
            case true when !appSetting.FavoriteWorkIds.Contains(id):
                appSetting.FavoriteWorkIds.Add(id);
                appSetting.Save();
                break;
            case false when appSetting.FavoriteWorkIds.Contains(id):
                appSetting.FavoriteWorkIds.Remove(id);
                appSetting.Save();
                break;
        }
    }

    public List<WorkInfo> GetFavorites()
    {
        try
        {
            lock (locker)
            {
                var works = GetWorks();
                var items = works.Where(a => appSetting.FavoriteWorkIds.Contains(a.Id))
                    .Select(a => new WorkInfo
                    {
                        Id = a.Id,
                        Title = a.Title ?? string.Empty,
                        Author = a.Author,
                        Content = a.Content,
                        Dynasty = a.Dynasty
                    }).ToList();
                logger.LogInformation($"{nameof(GetFavorites)} {Serializer.Serialize(items.Select(a => a.Id))}");
                return items;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return [];
    }

    public WorkInfo? GetWork(int id)
    {
        try
        {
            lock (locker)
            {
                var items = connection.Query<Work>(
                    $"select id as Id, title as Title, author as Author, content as Content, dynasty as Dynasty" +
                    $", intro as Intro, translation as Translation" +
                    $" from works where id = {id}");
                var find = items.FirstOrDefault(a => a.Id == id);
                if (find != null)
                    return new WorkInfo
                    {
                        Id = find.Id, Title = find.Title,
                        Author = find.Author,
                        Dynasty = find.Dynasty,
                        Content = find.Content,
                        Intro = find.Intro,
                        Translation = find.Translation
                    };
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
}