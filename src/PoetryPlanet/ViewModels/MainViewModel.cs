using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PoetryPlanet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
    
    [ObservableProperty]
    private Thickness _Margin = new Thickness(10) ;
}
