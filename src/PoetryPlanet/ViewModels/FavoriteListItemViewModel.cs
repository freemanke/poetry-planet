using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class FavoriteListItemViewModel : ViewModelBase
{
  
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? authorAndDynasty = "苏轼 · 宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂，左牵黄，右擎苍，锦帽貂裘，千骑卷平冈。";
    [ObservableProperty] private bool isFavorite = true;
    [ObservableProperty] private IBrush favoriteColor = AppSetting.UnFavoriteColorBrush;

    [RelayCommand]
    private void UnFavorite()
    {
        IsFavorite = false;
        poetryService.Favorite(Id, IsFavorite);
        FavoriteColor = IsFavorite ? AppSetting.FavoriteColorBrush : AppSetting.UnFavoriteColorBrush;
    }

    public WorkViewModel CreateWorkViewModel()
    {
        var work = poetryService.GetWork(Id);
        if (work == null) return new WorkViewModel();
        var vm = new WorkViewModel
        {
            Id = work.Id,
            Title = work.Title,
            Author = work.Author,
            Content = work.Content,
            Dynasty = work.Dynasty,
            Intro = work.Intro ?? "暂无介绍",
            Translation = work.Translation ?? "暂无译文",
        };
        return vm;
    }
}