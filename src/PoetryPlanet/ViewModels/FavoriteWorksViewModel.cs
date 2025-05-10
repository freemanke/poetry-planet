using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using PoetryPlanet.Dtos;
using PoetryPlanet.Services;

namespace PoetryPlanet.ViewModels;

public partial class FavoriteWorksViewModel : ViewModelBase
{
    private readonly PoetryService poetryService;
    [ObservableProperty] private ObservableCollection<FavoriteWorkViewModel> works = [];
    [ObservableProperty] private string keyword = "";

    public FavoriteWorksViewModel(PoetryService poetryService)
    {
        this.poetryService = poetryService;
        CreateDefault();
    }

    [RelayCommand]
    private void BackToFirstView() => MobileNavigation.Pop();

    public void DoGetFavoriteWorks()
    {
        var items = poetryService.GetFavoriteWorks().Select(a => new FavoriteWorkViewModel
        {
            Id = a.Id,
            Title = a.Title,
            Author = a.Author,
            Dynasty = a.Dynasty,
            Content = a.Content,
            Intro = a.Intro
        });
        Works.Clear();
        Works.AddRange(items);
    }

    private void CreateDefault()
    {
        Works.Clear();
        Works.Add(new FavoriteWorkViewModel
        {
            Id = 1,
            Title = "江城子 · 密州出猎",
            Author = "苏轼",
            Dynasty = "宋",
            Content = "老夫聊发少年狂，左迁龙右擒苍",
            Intro = "简介"
        });
        Works.Add(new FavoriteWorkViewModel
        {
            Id = 1,
            Title = "观书有感",
            Author = "朱熹",
            Dynasty = "宋",
            Content = "半亩方塘一鉴开，天光云影共徘徊",
            Intro = "简介"
        });
    }
}
