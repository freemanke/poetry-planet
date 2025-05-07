using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Dtos;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class WorkViewModel : ViewModelBase
{
    [ObservableProperty] private int id;
    [ObservableProperty] private string? title;
    [ObservableProperty] private string? author;
    [ObservableProperty] private string? content;
    [ObservableProperty] private string? intro;
}

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<WorkViewModel> works = [];
    
    public MainViewModel()
    {
        LoadWorks();
    }

    [ObservableProperty]
    private string _greeting = "第一个跨平台应用";
    
    [ObservableProperty]
    private string _firstTitle = "第一视图";
    
    [ObservableProperty]
    private string _secondTitle = "第二视图";

    [RelayCommand]
    private async Task OpenNextView()
    {
        MobileNavigation.Push(new GuideSecondView());
        await Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task OpenFirstView()
    {
        MobileNavigation.Push(new GuideFirstView());
        await Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task OpenNavigationView()
    {
        MobileNavigation.Push(new NavigationView());
        await Task.CompletedTask;
    }

    public void LoadWorks()
    {
        var docRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://home.freemanke.com:60011");
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/works?count=1");
        var response = httpClient.SendAsync(request).Result;
        var info = new[] { new WorkInfo { Id = 10, Author = "我", Content = "一首诗", Intro = "intro"} };
        var json = response.Content.ReadAsStringAsync().Result; // System.Text.Json.JsonSerializer.Serialize(info);
        var infos = response.Content.ReadFromJsonAsync<List<WorkInfo>>().Result;
        File.WriteAllText(Path.Combine(docRoot, "works.json"), json);
        Thread.Sleep(100);
        json = File.ReadAllText(Path.Combine(docRoot, "works.json"));
        var workInfos = System.Text.Json.JsonSerializer.Deserialize<List<WorkInfo>>(json);
        Thread.Sleep(100);
        Works.Clear();
        foreach (var item in workInfos!)
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
