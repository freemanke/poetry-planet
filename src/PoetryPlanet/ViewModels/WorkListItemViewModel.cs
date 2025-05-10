using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Dtos;
using PoetryPlanet.Services;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class WorkListItemViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? authorAndDynasty = "苏轼 · 宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂，左牵黄，右擎苍，锦帽貂裘，千骑卷平冈。为报倾城随太守，亲射虎，看孙郎。酒酣胸胆尚开张。鬓微霜，又何妨！持节云中，何日遣冯唐？会挽雕弓如满月，西北望，射天狼。";
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

    public static WorkListItemViewModel Create(WorkListItemInfo item)
    {
        return new WorkListItemViewModel
        {
            Id = item.Id, Title = item.Title,
            AuthorAndDynasty = $"{item.Author} · {item.Dynasty}",
            Content = item.Content
        };
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