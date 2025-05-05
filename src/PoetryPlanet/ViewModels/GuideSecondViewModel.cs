using System.Threading.Tasks;
using Avalonia;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class GuideSecondViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "导出向导：第二步";

    [RelayCommand]
    private async Task Previous()
    {
        MobileNavigation.Pop();
        await Task.CompletedTask;
    }
}
