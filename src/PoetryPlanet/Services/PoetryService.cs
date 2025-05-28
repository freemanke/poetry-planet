using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Dapper;
using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using PoetryPlanet.Data;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private object locker = new();
    [UsedImplicitly] private HttpClient httpClient;
    private SqliteConnection connection;
    private readonly AppSetting appSetting;
    private readonly ILogger<PoetryService> logger;
    private const string worksRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private const string collectionListRoute = "/api/v1/collections";
    private List<CollectionInfo> collectionCache = [];
    private List<WorkListItemInfo> workListCache = [];

    public PoetryService(ILogger<PoetryService> logger, AppSetting appSetting)
    {
        this.logger = logger;
        this.appSetting = appSetting;
        
        if(!File.Exists(AppSetting.SQLiteFilePath)) File.Copy(AppSetting.SQLiteFileName, AppSetting.SQLiteFilePath);
        connection = new SqliteConnection($"DataSource={AppSetting.SQLiteFilePath};Cache=Shared");
        
        var rootPath = OperatingSystem.IsAndroid()
            ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        logger.LogInformation("Current data root path: {}", rootPath);

        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://home.freemanke.com:60011") };
    }

    public List<WorkListItemInfo> GetWorkList()
    {
        if (workListCache.Count > 0) return workListCache;

        try
        {
            lock (locker)
            {
                var works = new List<WorkListItemInfo>();
                var items = connection.Query(
                        "select id as Id, title as Title, author as Author, content as Content, dynasty as Dynasty  from works")
                    .ToWorks().Select(a => new WorkListItemInfo
                    {
                        Id = a.Id, Title = a.Title, Author = a.Author, Content = a.Content, Dynasty = a.Dynasty
                    }).ToList();
                workListCache.AddRange(items);
            }

            return workListCache;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return [];
    }

    public List<WorkListItemInfo> GetWorkList(int collectionId)
    {
        try
        {
            lock (locker)
            {
                var workIds = connection.Query(
                        $"select work_id as WorkId from collection_works where collection_id={collectionId}")
                    .ToCollectionWorks().Select(a => a.WorkId).ToList();
                return workListCache.Where(a => workIds.Contains(a.Id)).ToList();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return [];
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

    public List<WorkInfo> GetFavoriteWorks()
    {
        try
        {
            lock (locker)
            {
                var items = workListCache.Where(a =>
                        appSetting.FavoriteWorkIds.Contains(a.Id))
                    .Select(a =>new WorkInfo
                    {
                        Id = a.Id, Author = a.Author, Content = a.Content, Dynasty = a.Dynasty
                    }).ToList();
                logger.LogInformation("Get favorites \"{}\"", string.Join(",", items.Select(a => a.Title)));
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