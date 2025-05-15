using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Controls;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class MineViewModel : ViewModelBase
{
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