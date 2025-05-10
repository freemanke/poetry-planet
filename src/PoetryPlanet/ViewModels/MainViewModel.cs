using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string firstTitle = "向导试图一";
    [ObservableProperty] private string secondTitle = "向导试图二";

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
}
