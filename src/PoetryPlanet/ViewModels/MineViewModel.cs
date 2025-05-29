using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Controls;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class MineViewModel : ViewModelBase
{
    [ObservableProperty] private string fileSetting;
    [ObservableProperty] private string memorySetting;
    [ObservableProperty] private List<LogViewModel> logs = [];

    public MineViewModel()
    {
        FileSetting = JsonSerializer.Serialize(AppSetting.Load());
        memorySetting = JsonSerializer.Serialize(appSetting);
    }
    
    [RelayCommand]
    private void RefreshSetting()
    {
        FileSetting = JsonSerializer.Serialize(AppSetting.Load());
        MemorySetting = JsonSerializer.Serialize(appSetting);
    }

    [RelayCommand]
    private void RefreshLogs()
    {
        Logs = File.ReadAllLines(AppSetting.LogFilePath).Reverse()
            .Take(500).Select(a => new LogViewModel { Log = a })
            .ToList();
    }

    [RelayCommand]
    private void ShowDialog()
    {
        InteractiveContainer.ShowDialog(new MyDialogView(), true);
    }
    
    [RelayCommand]
    private void ShowToast()
    {
        InteractiveContainer.ShowToast(new TextBlock { Text = "操作已完成" }, 2);
    }
    
    [RelayCommand]
    private void ChangeTheme()
    {
        Application.Current!.RequestedThemeVariant = 
            Application.Current.RequestedThemeVariant == ThemeVariant.Dark
            ? ThemeVariant.Light
            : ThemeVariant.Dark;
    }
}