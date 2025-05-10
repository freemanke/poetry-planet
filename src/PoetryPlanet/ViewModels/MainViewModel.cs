using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
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
