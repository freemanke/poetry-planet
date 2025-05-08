using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.Services;

public class PoetryService
{
    private const string workListRoute = "/api/v1/work_list";
    private const string workRoute = "/api/v1/works";
   private readonly HttpClient httpClient = new();
   
   public PoetryService()
   {
       httpClient.BaseAddress = new Uri("https://home.freemanke.com:60011");
   }
    
    public List<WorkListItemInfo> GetWorkList()
    {
        var works = new List<WorkListItemInfo>();
        var stamp = new WorkListItemInfo
            { Id = 10, Title = "", Author = "我", Dynasty = "", AuthorId = 0, AuthorRemoteId = "" };
        Console.WriteLine($"在IOS环境下，反序列化对象前，需要创建一个对象，否则会反序列化报错：{stamp}");
        try
        {
            var docRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var request = new HttpRequestMessage(HttpMethod.Get, workListRoute);
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var infos = response.Content.ReadFromJsonAsync<List<WorkListItemInfo>>().Result;
            var filePath = Path.Combine(docRoot, "works.json");
            File.WriteAllText(filePath, json);
            Console.WriteLine($"文件已保存到：{filePath}");
            if (infos != null) works.AddRange(infos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return works;
    }
    
    public WorkInfo GetWork(int id)
    {
        var work = new WorkInfo
            { Id = 10, Title = "标题", Author = "作者", Dynasty = "年代", AuthorId = 0, AuthorRemoteId = "",Content = "内容"};
        Console.WriteLine($"在IOS环境下，反序列化对象前，需要创建一个对象，否则会反序列化报错：{work}");
        try
        {
            var docRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var request = new HttpRequestMessage(HttpMethod.Get, workRoute + $"?id={id}");
            var response = httpClient.SendAsync(request).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var find = response.Content.ReadFromJsonAsync<WorkInfo>().Result;
            var filePath = Path.Combine(docRoot, "work.json");
            File.WriteAllText(filePath, json);
            Console.WriteLine($"文件已保存到：{filePath}");
            if (find != null) work = find;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return work;
    }
}