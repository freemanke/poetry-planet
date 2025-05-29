using CommunityToolkit.Mvvm.ComponentModel;

namespace PoetryPlanet.ViewModels;

public partial class LogViewModel : ViewModelBase
{
    [ObservableProperty] private string log = "";
}