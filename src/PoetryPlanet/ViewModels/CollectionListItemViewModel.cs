using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Controls;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class CollectionListItemViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string name = "小学生诗词";
    [ObservableProperty] private string kind = "书籍";
    [ObservableProperty] private string desc = "描述信息";
    [ObservableProperty] private string title = "小学生诗词";
    [ObservableProperty] private bool isFavorite;

    [RelayCommand]
    private void ShowCollection()
    {
        logger.LogInformation("Show collection");
        MobileNavigation.Push(new CollectionView
        {
            DataContext = new CollectionViewModel(),
        });
    }

    public CollectionViewModel Create()
    {
        return new CollectionViewModel
        {
            Id = Id, Desc = Desc, Name = Name,
            WorkList = new ObservableCollection<WorkListItemViewModel>()
        };
    }
}