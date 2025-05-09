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
        Task.Run(() => PoetryService.Instance.GetWorks());
    }

    public void DoGetWorkList()
    {
        Console.WriteLine($"开始获取作品列表，关键字：\"{Keyword}\"");
        var workInfos = PoetryService.Instance.GetWorkList();
        var items = workInfos.Where(a =>
                a.Title!.Contains(Keyword)
                || a.Content!.Contains(Keyword)
                || a.Author!.Contains(Keyword))
            .Select(item =>
                new WorkListItemViewModel
                {
                    Id = item.Id, Title = item.Title,
                    AuthorAndDynasty = $"{item.Author} · {item.Dynasty}",
                    Content = item.Content
                }).ToList();

        WorkList.Clear();
        WorkList.AddRange(items);
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
