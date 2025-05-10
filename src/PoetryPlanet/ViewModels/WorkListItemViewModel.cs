using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Services;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class WorkListItemViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? authorAndDynasty = "苏轼 · 宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂，左迁龙右擒苍";
    [ObservableProperty] private bool isFavorite;
    [ObservableProperty] private IBrush favoriteBrush = new SolidColorBrush(Colors.LightGray);

    public WorkViewModel CreateViewModel()
    {
        logger.LogInformation($"当前线程：{Environment.CurrentManagedThreadId}");
        var work = poetryService.GetWork(Id);
        var vm = new WorkViewModel
        {
            Id = work.Id,
            Title = work.Title,
            Author = work.Author,
            Content = work.Content,
            Dynasty = work.Dynasty,
            Intro = work.Intro,
            Translation = work.Translation,
        };
        IsFavorite = work.IsFavorite;
        return vm;
    }

    [RelayCommand]
    private void Favorite()
    {
        logger.LogInformation($"{(IsFavorite?"取消收藏":"收藏")}作品：{Title}");
        IsFavorite = !IsFavorite;
        poetryService.Favorite(Id, IsFavorite);
        FavoriteBrush = IsFavorite ? new SolidColorBrush(Colors.LawnGreen) : new SolidColorBrush(Colors.LightGray);
    }
}