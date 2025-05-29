using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Controls;

namespace PoetryPlanet.ViewModels;

public partial class FavoriteListViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<FavoriteListItemViewModel> workList = [];

    public FavoriteListViewModel()
    {
        CreateDefault();
    }

    [RelayCommand]
    private void BackToFirstView() => MobileNavigation.Pop();

    public void DoGetFavoriteWorks()
    {
        var items = poetryService.GetFavorites().Select(a => new FavoriteListItemViewModel
        {
            Id = a.Id,
            Title = a.Title,
            AuthorAndDynasty = $"{a.Author}·{a.Dynasty}",
            Content = a.Content,
            IsFavorite = true,
            FavoriteColor = AppSetting.FavoriteColorBrush,
        }).ToList();
        WorkList.Clear();
        WorkList.AddRange(items);
        logger.LogInformation($"{nameof(DoGetFavoriteWorks)} {Serializer.Serialize(items.Select(a => a.Id))} " +
                              $"{Serializer.Serialize(WorkList.Select(a => a.Id))}");
    }

    private void CreateDefault()
    {
        WorkList.Clear();
        WorkList.Add(new FavoriteListItemViewModel
        {
            Id = 1,
            Title = "江城子 · 密州出猎",
            AuthorAndDynasty = "苏轼 宋",
            Content = "老夫聊发少年狂，左迁龙右擒苍",
        });
        WorkList.Add(new FavoriteListItemViewModel
        {
            Id = 1,
            Title = "观书有感",
            AuthorAndDynasty = "朱熹 宋",
            Content = "半亩方塘一鉴开，天光云影共徘徊",
        });
    }
}
