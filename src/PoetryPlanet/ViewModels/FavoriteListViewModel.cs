using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using PoetryPlanet.Controls;
using PoetryPlanet.Dtos;
using PoetryPlanet.Services;

namespace PoetryPlanet.ViewModels;

public partial class FavoriteListViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<FavoriteViewModel> workList = [];
    [ObservableProperty] private string keyword = "";

    public FavoriteListViewModel()
    {
        CreateDefault();
    }

    [RelayCommand]
    private void BackToFirstView() => MobileNavigation.Pop();

    public void DoGetFavoriteWorks()
    {
        var items = poetryService.GetFavoriteWorks().Select(a => new FavoriteViewModel
        {
            Id = a.Id,
            Title = a.Title,
            Author = $"{a.Author}·{a.Dynasty}" ,
            Dynasty = a.Dynasty,
            Content = a.Content,
            Intro = a.Intro??"暂无介绍",
            Translation = a.Translation??"暂无译文",
        });
        WorkList.Clear();
        WorkList.AddRange(items);
    }

    private void CreateDefault()
    {
        WorkList.Clear();
        WorkList.Add(new FavoriteViewModel
        {
            Id = 1,
            Title = "江城子 · 密州出猎",
            Author = "苏轼",
            Dynasty = "宋",
            Content = "老夫聊发少年狂，左迁龙右擒苍",
            Intro = "简介"
        });
        WorkList.Add(new FavoriteViewModel
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
