using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string firstTitle = "向导试图一";
    [ObservableProperty] private string secondTitle = "向导试图二";

    public MainViewModel()
    {
        logger.LogInformation("创建主视图模型");
    }

    [RelayCommand]
    private void OpenNavigationView()
    {
        logger.LogInformation("打开导航视图");
        MobileNavigation.Push(new NavigationView());
    }
}
