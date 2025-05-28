using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Nelibur.ObjectMapper;
using PoetryPlanet.Data;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private readonly ILogger<PoetryService> logger;
    private readonly AppSetting appSetting;
    private const string worksRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private const string collectionListRoute = "/api/v1/collections";
    private List<CollectionInfo> collectionCache = [];
    private HttpClient httpClient;
    private List<WorkListItemInfo> workListCache = [];
    private static readonly char[] separator = ['。', '；'];
    private object locker = new();
    private SqliteConnection connection;

    public PoetryService(ILogger<PoetryService> logger, AppSetting appSetting)
    {
        this.logger = logger;
        this.appSetting = appSetting;
        
        var sqliteFilePath = Path.Combine(AppSetting.ConfigRootPath, "poetry-planet.sqlite");
        if(!File.Exists(sqliteFilePath)) File.Copy("/Users/freeman/Downloads/poetry-planet.sqlite", sqliteFilePath);
        connection = new SqliteConnection($"DataSource={sqliteFilePath};Cache=Shared");
        
        var rootPath = OperatingSystem.IsAndroid()
            ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        logger.LogInformation("Current data root path: {}", rootPath);

        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://home.freemanke.com:60011") };

        // 在IOS环境下，反序列化对象前，需要创建一个对象，否则会反序列化报错
        var stamp = new WorkListItemInfo { Id = 10, Title = "", Author = "我", Dynasty = "", Content = "诗词内容", };
        var work = new WorkInfo
        {
            Id = 10, Title = "标题", Author = "作者", Dynasty = "年代", Content = "内容", Intro = "", IsFavorite = false,
            Translation = ""
        };
        var collection = new CollectionInfo { Name = "*", Desc = "*", Id = 10, Kind = "*" };
    }

    public List<WorkListItemInfo> GetWorkList()
    {
        if (workListCache.Count > 0) return workListCache;

        try
        {
            lock (locker)
            {
                var works = new List<WorkListItemInfo>();
                var items = connection.Query("select id as Id, title as Title from works");
                foreach (var item in items )
                {
                    var work = new WorkListItemInfo();
                    foreach (KeyValuePair<string,object> i in item)
                    {
                        if (i.Key == "Id") work.Id = int.Parse(i.Value.ToString() ?? "0");
                        if (i.Key == "Title") work.Title = i.Value.ToString();
                    }
                    works.Add(work);
                }
                /*works = items.Select(a => new WorkListItemInfo
                {
                    Id = a.Id, Title = a.Title, Author = a.Author,
                    AuthorId = a.AuthorId,
                    Content = a.Content.Split(separator).FirstOrDefault() ?? "",
                    Dynasty = a.Dynasty
                }).ToList();*/
                workListCache.AddRange(works);
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
        return new List<WorkListItemInfo>();
        try
        {
            lock (locker)
            {
                var items = connection.Query<CollectionWork>("select * from collection_works").Where(a => a.CollectionId == collectionId).Select(a => a.WorkId).ToList();
                return workListCache.Where(a => items.Contains(a.Id)).ToList();
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
        return new List<CollectionInfo>();
        try
        {
            lock (locker)
            {
                var items = connection.Query<Collection>("select * from collections");
                return items.Select(a => new CollectionInfo()
                {
                    Id = a.Id, Kind = a.Kind, Name = a.Name, Desc = a.Desc
                }).ToList();
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
        return;
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
        return new List<WorkInfo>();
        try
        {
            lock (locker)
            {
                var items = connection.Query<Work>("select * from works").Where(a =>
                        appSetting.FavoriteWorkIds.Contains(a.Id))
                    .Select(a => TinyMapper.Map<WorkInfo>(a)).ToList();
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
        return null;
        try
        {
            lock (locker)
            {
                var items = connection.Query<Work>("select id, title from works");
                var find = items.FirstOrDefault(a => a.Id == id);
                return find != null
                    ? new WorkInfo
                    {
                        Id = find.Id, Title = find.Title,
                        Author = find.Author, Dynasty = find.Dynasty, Content = find.Content
                    }
                    : null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
}