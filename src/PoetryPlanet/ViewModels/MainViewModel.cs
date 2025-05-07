using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls.Platform;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PoetryPlanet.Dtos;
using PoetryPlanet.Views;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PoetryPlanet.ViewModels;

public partial class WorkViewModel : ViewModelBase
{
    [ObservableProperty] private string title = "";
    [ObservableProperty] private string author = "";
    [ObservableProperty] private string content = "";

}

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<WorkViewModel> _works = [];

    public MainViewModel()
    {
        var docRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://home.freemanke.com:60011");
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/works?count=5");
        var response = httpClient.SendAsync(request).Result;
        var json = response.Content.ReadAsStringAsync().Result;
        var getWorkResponse = JsonConvert.DeserializeObject<GetWorkResponse>(json);
        _works.Clear();
        foreach (var item in getWorkResponse!.Works)
        {
            _works.Add(new WorkViewModel
            {
                Title = item.Title,
                Author = item.Author,
                Content = item.Content
            });
        }
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

    [RelayCommand]
    private async Task BackToFirstView()
    {
        MobileNavigation.Pop();
        await Task.CompletedTask;
    }

    [ObservableProperty] private string _poetry = "";
}
