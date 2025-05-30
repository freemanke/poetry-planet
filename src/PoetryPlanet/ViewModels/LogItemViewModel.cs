using CommunityToolkit.Mvvm.ComponentModel;

namespace PoetryPlanet.ViewModels;

public partial class LogItemViewModel : ViewModelBase
{
    [ObservableProperty] private string log = "日志信息";
}