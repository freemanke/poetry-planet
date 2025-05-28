using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Dtos;

namespace PoetryPlanet.ViewModels;

public partial class FavoriteListItemViewModel : ViewModelBase
{
    private static readonly SolidColorBrush FavoriteColorBrush = new(Colors.MediumSeaGreen);
    private static readonly SolidColorBrush UnFavoriteColorBrush = new(Colors.LightGray);

    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? authorAndDynasty = "苏轼 · 宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂，左牵黄，右擎苍，锦帽貂裘，千骑卷平冈。";
    [ObservableProperty] private bool isFavorite;
    [ObservableProperty] private IBrush favoriteColor = UnFavoriteColorBrush;

    public FavoriteListItemViewModel(){}
    
    [RelayCommand]
    private void Favorite()
    {
        logger.LogInformation("{} work \"{}\"", IsFavorite ? "Unfavorite" : "Favorite", Title);
        IsFavorite = !IsFavorite;
        poetryService.Favorite(Id, IsFavorite);
        FavoriteColor = IsFavorite ? FavoriteColorBrush : UnFavoriteColorBrush;
    }

    public WorkViewModel CreateWorkViewModel()
    {
        var work = poetryService.GetWork(Id);
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

    public static WorkListItemViewModel Create(WorkListItemInfo item, bool isFavorite)
    {
        return new WorkListItemViewModel
        {
            Id = item.Id, Title = item.Title,
            AuthorAndDynasty = $"{item.Author} · {item.Dynasty}",
            Content = item.Content,
            IsFavorite = isFavorite,
            FavoriteColor = isFavorite ? FavoriteColorBrush : UnFavoriteColorBrush,
        };
    }
}