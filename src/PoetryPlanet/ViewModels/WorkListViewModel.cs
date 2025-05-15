using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.Extensions.Logging;
using MobileNavigation = PoetryPlanet.Controls.MobileNavigation;

namespace PoetryPlanet.ViewModels;

public partial class WorkListViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<WorkListItemViewModel> workList = [];
    [ObservableProperty] private string keyword = "";

    public WorkListViewModel()
    {
        CreateDefault();
    }

    [RelayCommand]
    private void GetWorkList() => Task.Run(() => DoGetWorkList());

    [RelayCommand]
    private void SearchWorkList() => Task.Run(() => DoGetWorkList());

    [RelayCommand]
    private void BackToFirstView() => MobileNavigation.Pop();

    public void DoGetWorks()
    {
        Task.Run(() =>
        {
            var workInfos = poetryService.GetWorks();
            workInfos.ForEach(a => a.IsFavorite = appSetting.FavoriteWorkIds.Contains(a.Id));
        });
    }

    public void DoGetWorkList()
    {
        logger.LogInformation($"Get works by keyword \"{Keyword}\"");
        var workInfos = poetryService.GetWorkListItems();
        var items = workInfos.Where(a =>
                a.Title!.Contains(Keyword)
                || a.Content!.Contains(Keyword)
                || a.Dynasty!.Contains(Keyword)
                || a.Author!.Contains(Keyword))
            .Select(item => WorkListItemViewModel.Create(item)).ToList();
        WorkList.Clear();
        WorkList.AddRange(items);
        return;

        bool IsFavorite(int id) => appSetting.FavoriteWorkIds.Contains(id);
    }

    private void CreateDefault()
    {
        WorkList.Clear();
        WorkList.Add(new WorkListItemViewModel
        {
            Id = 1,
            Title = "江城子 · 密州出猎",
            AuthorAndDynasty = "苏轼 · 宋",
            Content = "老夫聊发少年狂，左迁龙右擒苍"
        });
        WorkList.Add(new WorkListItemViewModel
        {
            Id = 1,
            Title = "观书有感",
            AuthorAndDynasty = "朱熹 · 宋",
            Content = "半亩方塘一鉴开，天光云影共徘徊"
        });
    }
}
