using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class LogsViewModel : ViewModelBase
{
    [ObservableProperty] private List<LogItemViewModel> logs = [];

    public LogsViewModel()
    {
        Logs = [new LogItemViewModel { Log = "点击刷新按钮查看日志..." }];
    }
    
    [RelayCommand]
    private void RefreshLogs()
    {
        Logs = File.ReadAllLines(AppSetting.LogFilePath).Reverse()
            .Take(500).Select(a => new LogItemViewModel { Log = a })
            .ToList();
    }
}