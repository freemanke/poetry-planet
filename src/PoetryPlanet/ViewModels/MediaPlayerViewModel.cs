using CommunityToolkit.Mvvm.ComponentModel;

namespace PoetryPlanet.ViewModels;

public partial class MediaPlayerViewModel : ViewModelBase
{
    
    [ObservableProperty]
    public string greeting = "hello";

}