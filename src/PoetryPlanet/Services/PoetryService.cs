using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private const string workRoute = "/api/v1/works";
    private const string workListRoute = "/api/v1/work_list";
    private readonly string rootPath;
    private readonly string workListFilePath;
    private readonly HttpClient httpClient = new();

    public PoetryService()
    {
        rootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        workListFilePath = Path.Combine(rootPath, "work_list.json");
        httpClient.BaseAddress = new Uri("https://home.freemanke.com:60011");

        // 在IOS环境下，反序列化对象前，需要创建一个对象，否则会反序列化报错
        var stamp = new WorkListItemInfo { Id = 10, Title = "", Author = "我", Dynasty = "" };
        var work = new WorkInfo { Id = 10, Title = "标题", Author = "作者", Dynasty = "年代", Content = "内容" };
    }

    public List<WorkListItemInfo> GetWorkList()
    {
        var works = new List<WorkListItemInfo>();
        if (TryGet<List<WorkListItemInfo>>(workListFilePath, out var workList)
            && workList != null) return workList;

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, workListRoute);
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var infos = JsonSerializer.Deserialize<List<WorkListItemInfo>>(json);

            File.WriteAllText(workListFilePath, json);
            Console.WriteLine($"文件已保存到：{workListFilePath}");
            if (infos != null) works.AddRange(infos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return works;
    }

    private bool TryGet<T>(string jsonFilePath, out T? value) where T : class
    {
        value = null;
        if (File.Exists(jsonFilePath))
        {
            try
            {
                var json = File.ReadAllText(jsonFilePath);
                var find = JsonSerializer.Deserialize<T>(json);
                value = find;
                return find != null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        return false;
    }

    public WorkInfo GetWork(int id)
    {
        var filePath = Path.Combine(rootPath, $"{id}.json");
        if (TryGet<WorkInfo>(filePath, out var value)
            && value != null) return value;

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, workRoute + $"?id={id}");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var find = response.Content.ReadFromJsonAsync<WorkInfo>().Result;
            File.WriteAllText(filePath, json);
            Console.WriteLine($"文件已保存到：{filePath}");
            if (find != null) return find;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new WorkInfo();
    }
}