using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Avalonia.Controls.Platform;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PoetryPlanet.Data.Models;
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
        var rootPath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!,
            OperatingSystem.IsIOS() ? "PoetryPlanet.iOS.app" : "");
        var items = JsonConvert.DeserializeObject<List<WorkViewModel>>(File.ReadAllText(Path.Combine(rootPath, "works.json")));

        _works.Clear();
        foreach (var item in items!)
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
