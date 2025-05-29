using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{

    private readonly AppSetting appSetting;
    private readonly SQLiteService sqlite;
    private readonly ILogger<PoetryService> logger;
    private const string worksRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private const string collectionListRoute = "/api/v1/collections";
    private List<CollectionInfo> collectionCache = [];
    private readonly List<WorkInfo> worksCache = [];
    private readonly Lock locker = new();

    public PoetryService(ILogger<PoetryService> logger, AppSetting appSetting, SQLiteService sqlite)
    {
        this.logger = logger;
        this.appSetting = appSetting;
        this.sqlite = sqlite;
        logger.LogInformation("Root path {}", AppSetting.ConfigRootPath);
    }

    private List<WorkInfo> GetWorks()
    {
        if (worksCache.Count != 0) return worksCache;
        var works = sqlite.GetWorks();
        worksCache.Clear();
        worksCache.AddRange(works);
        return worksCache;
    }

    public List<WorkListItemInfo> GetWorkList()
    {
        var works = GetWorks();
        var items = works.Select(a => new WorkListItemInfo
        {
            Id = a.Id,
            Title = a.Title ?? string.Empty,
            Author = a.Author ?? string.Empty,
            Content = a.Content ?? string.Empty,
            Dynasty = a.Dynasty ?? string.Empty
        }).ToList();

        logger.LogInformation("{} {}", nameof(GetWorkList), items.Count);
        return items;
    }

    public List<WorkListItemInfo> GetWorkList(int collectionId)
    {
        var infos = sqlite.GetCollectionWorks(collectionId);
        var workList = GetWorkList();
        var items = workList.Where(a => infos.Select(b => b.WorkId).Contains(a.Id)).ToList();
        logger.LogInformation("{} {}", nameof(GetWorkList), items.Count);
        return items;
    }

    public List<CollectionInfo> GetCollectionList()
    {
        return sqlite.GetCollections();
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
        logger.LogInformation("{} {}", nameof(GetFavorites), Serializer.Serialize(items.Select(a => a.Id)));
        return items;
    }

    public WorkInfo? GetWork(int id)
    {
        return sqlite.GetWork(id);
    }
}