using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Data.Models;
using PoetryPlanet.Dtos;
#pragma warning disable CA2254

namespace PoetryPlanet.Services;

public class SQLiteService
{
    private readonly ILogger<SQLiteService> logger;
    private readonly SqliteConnection connection;
    private readonly Lock locker = new();

    public SQLiteService(ILogger<SQLiteService> logger)
    {
        this.logger = logger;
        if (!File.Exists(AppSetting.SQLiteFilePath))
        {
            File.Copy(AppSetting.SQLiteFileName, AppSetting.SQLiteFilePath);
            logger.LogInformation(
                $"copy init database from {AppSetting.SQLiteFileName} to {AppSetting.SQLiteFilePath}");
        }

        connection = new SqliteConnection($"DataSource={AppSetting.SQLiteFilePath};Cache=Shared");
    }

    public List<WorkInfo> GetWorks()
    {
        var items = new List<WorkInfo>();
        using (locker.EnterScope())
        {
            try
            {
                items = connection
                    .Query<Work>(
                        "select id as Id, title as Title, author as Author, content as Content" +
                        ", dynasty as Dynasty from works")
                    .Select(a => new WorkInfo
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Author = a.Author,
                        Content = a.Content.Split(['。', '；']).FirstOrDefault() ?? "",
                        Dynasty = a.Dynasty
                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        logger.LogInformation($"{nameof(GetWorks)} {items.Count}");
        return items;
    }

    public List<CollectionInfo> GetCollections()
    {
        var items = new List<CollectionInfo>();
        using (locker.EnterScope())
        {
            try
            {
                items = connection.Query<Collection>("select id as Id, name as Name, desc as Desc from collections")
                    .Select(a => new CollectionInfo
                    {
                        Id = a.Id, Name = a.Name, Desc = a.Desc
                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        logger.LogInformation($"{nameof(GetCollections)} {items.Count}");
        return items;
    }

    public List<CollectionWorkInfo> GetCollectionWorks(int collectionId = 0)
    {
        var items = new List<CollectionWorkInfo>();
        using (locker.EnterScope())
        {
            try
            {
                items = connection
                    .Query<CollectionWork>(
                        "select id as Id, work_id as WorkId, collection_id as CollectionId from collection_works" +
                        (collectionId <= 0 ? "" : $" where collection_id={collectionId}"))
                    .Select(a => new CollectionWorkInfo()
                    {
                        Id = a.Id, WorkId = a.WorkId, CollectionId = a.CollectionId
                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        logger.LogInformation($"{nameof(GetCollectionWorks)} {items.Count}");
        return items;
    }

    public WorkInfo? GetWork(int id)
    {
        WorkInfo? work = null;
        try
        {
            using (locker.EnterScope())
            {
                var items = connection.Query<Work>(
                    $"select id as Id, title as Title, author as Author, content as Content, dynasty as Dynasty" +
                    $", intro as Intro, translation as Translation" +
                    $" from works where id = {id}");
                var find = items.FirstOrDefault(a => a.Id == id);
                if (find != null)
                {
                    work = new WorkInfo
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
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        logger.LogInformation($"{nameof(GetWork)} {Serializer.Serialize(work?.Title)}");
        return work;
    }
}