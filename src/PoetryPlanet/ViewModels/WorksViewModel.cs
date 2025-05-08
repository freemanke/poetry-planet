using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.ViewModels;

public partial class WorksViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<WorkViewModel> works = [];
    
    [RelayCommand]
    private async Task LoadWorks()
    {
        await Task.Run(() => DoLoadWorks());
        await Task.CompletedTask;
    }

    public void DoLoadWorks()
    {
        var stamp = new WorkInfo { Id = 10, Title = "", Author = "我", Content = "一首诗", Intro = "intro" };
        Console.WriteLine($"在IOS环境下，反序列化对象前，需要创建一个对象，否则会反序列化报错：{stamp}");

        var docRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://home.freemanke.com:60011");
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/works?count=10");
        var response = httpClient.SendAsync(request).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        var infos = response.Content.ReadFromJsonAsync<List<WorkInfo>>().Result;
        var filePath = Path.Combine(docRoot, "works.json");
        File.WriteAllText(filePath, json);
        Console.WriteLine($"文件已保存到：{filePath}");
        Works.Clear();
        foreach (var item in infos!)
        {
            Works.Add(new WorkViewModel
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Content = item.Content,
                Intro = item.Intro
            });
        }
    }

    [RelayCommand]
    private async Task BackToFirstView()
    {
        MobileNavigation.Pop();
        await Task.CompletedTask;
    }

    [ObservableProperty] private string _poetry = "";
}
