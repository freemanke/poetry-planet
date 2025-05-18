using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Data;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private readonly ILogger<PoetryService> logger;
    private readonly AppSetting appSetting;
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;
    private const string worksRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private const string collectionListRoute = "/api/v1/collections";
    private List<CollectionInfo> collectionCache = [];
    private HttpClient httpClient;
    private List<WorkListItemInfo> workListCache = [];
    private static readonly char[] separator = ['。', '；'];

    public PoetryService(ILogger<PoetryService> logger, AppSetting appSetting,
        ApplicationDbContext db, IMapper mapper)
    {
        this.logger = logger;
        this.appSetting = appSetting;
        this.db = db;
        this.mapper = mapper;
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
        var works = db.Works.Select(a => new WorkListItemInfo
        {
            Id = a.Id, Title = a.Title, Author = a.Author,
            AuthorId = a.AuthorId,
            Content = a.Content.Split(separator).FirstOrDefault() ?? "",
            Dynasty = a.Dynasty
        }).ToList();
        workListCache.AddRange(works);
        return workListCache;
    }

    public List<WorkListItemInfo> GetWorkList(int collectionId)
    {
       var items =  db.CollectionWorks.Where(a => a.CollectionId == collectionId).Select(a=>a.WorkId).ToList();
       return workListCache.Where(a => items.Contains(a.Id)).ToList();
    }

    public List<CollectionInfo> GetCollectionList()
    {
        return db.Collections.Select(a => mapper.Map<CollectionInfo>(a)).ToList();
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
        var items = db.Works.Where(a =>
                appSetting.FavoriteWorkIds.Contains(a.Id))
            .Select(a => mapper.Map<WorkInfo>(a)).ToList();
        logger.LogInformation("Get favorites \"{}\"", string.Join(",", items.Select(a => a.Title)));
        return items;
    }

    public WorkInfo? GetWork(int id)
    {

        var find = db.Works.FirstOrDefault(a => a.Id == id);
        if (find != null) return mapper.Map<WorkInfo>(find);
        return null;
    }
}