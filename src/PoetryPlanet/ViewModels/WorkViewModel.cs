using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class WorkViewModel : ViewModelBase
{
    [ObservableProperty] private int id;
    [ObservableProperty] private string? title;
    [ObservableProperty] private string? author;
    [ObservableProperty] private string? content;
    [ObservableProperty] private string? intro;

    [RelayCommand]
    public void Favorite()
    {
        
    }
}