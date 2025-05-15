using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private readonly ILogger<PoetryService> logger;
    private readonly AppSetting appSetting;
    private readonly bool useCache;
    private const string worksRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private readonly string rootPath;
    private readonly string workListFilePath;
    private readonly string worksFilePath;
    private readonly HttpClient httpClient;
    private List<WorkInfo> workCache = [];

    public PoetryService(ILogger<PoetryService> logger, AppSetting appSetting, bool useCache = true)
    {
        this.logger = logger;
        this.appSetting = appSetting;
        this.useCache = useCache;
        rootPath = OperatingSystem.IsAndroid()
            ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        workListFilePath = Path.Combine(rootPath, "work_list.json");
        worksFilePath = Path.Combine(rootPath, "works.json");
        logger.LogInformation($"当前数据文件存储根目录：{rootPath}");

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
    }
    
    public List<WorkListItemInfo> GetWorkListItems()
    {
        var works = new List<WorkListItemInfo>();
        if (useCache
            && TryGet<List<WorkListItemInfo>>(workListFilePath, out var workList)
            && workList != null && workList.Count != 200)
        {
            logger.LogInformation($"Local cached works count {workList.Count}");
            return workList;
        }

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, workListRoute + "?count=10000000");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var infos = JsonSerializer.Deserialize<List<WorkListItemInfo>>(json);

            File.WriteAllText(workListFilePath, json);
            if (infos != null)
            {
                logger.LogInformation($"通过接口获取到作品列表 {infos.Count} 文件已保存到 {workListFilePath}");
                works.AddRange(infos);
            }
        }
        catch (Exception e)
        {
            logger.LogError($"获取作品列表出错，{e.Message}");
        }

        return works;
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
        var items = workCache.Where(a => appSetting.FavoriteWorkIds.Contains(a.Id)).ToList();
        logger.LogInformation($"Get favorites \"{string.Join(",", items.Select(a => a.Title))}\"");
        return items;
    }

    public List<WorkInfo> GetWorks()
    {
        if (workCache.Count != 0) return workCache;
        try
        {
            string json;
            List<WorkInfo>? infos;
            if (File.Exists(worksFilePath))
            {
                json = File.ReadAllText(worksFilePath);
                infos = JsonSerializer.Deserialize<List<WorkInfo>>(json);
                if (infos != null && infos.Count != 0)
                {
                    workCache = infos;
                    return infos;
                }
            }

            var request = new HttpRequestMessage(HttpMethod.Get, worksRoute);
            var response = httpClient.SendAsync(request).Result;
            json = response.Content.ReadAsStringAsync().Result;
            infos = JsonSerializer.Deserialize<List<WorkInfo>>(json);

            File.WriteAllText(worksFilePath, json);
            logger.LogInformation($"文件已保存到：{worksFilePath}");
            if (infos != null) workCache = infos;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return workCache;
    }
    
    public WorkInfo GetWork(int id)
    {
        var first = workCache.FirstOrDefault(a => a.Id == id);
        if (first != null) return first;

        var filePath = Path.Combine(rootPath, $"{id}.json");
        if (useCache
            && TryGet<WorkInfo>(filePath, out var value)
            && value != null) return value;

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, worksRoute + $"/{id}");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var find = response.Content.ReadFromJsonAsync<WorkInfo>().Result;
            File.WriteAllText(filePath, json);
            logger.LogInformation($"文件已保存到：{filePath}");
            if (find != null) return find;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return new WorkInfo();
    }

    private bool TryGet<T>(string jsonFilePath, out T? value) where T : class
    {
        value = null;
        if (!File.Exists(jsonFilePath)) return false;
        try
        {
            var json = File.ReadAllText(jsonFilePath);
            var find = JsonSerializer.Deserialize<T>(json);
            value = find;
            return find != null;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }

        return false;
    }
}