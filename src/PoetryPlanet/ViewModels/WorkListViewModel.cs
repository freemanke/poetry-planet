using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Services;

namespace PoetryPlanet.ViewModels;

public partial class WorkListViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<WorkListItemViewModel> workList = [];
    
    [ObservableProperty] 
    private string keyword = "";

    public WorkListViewModel()
    {
        workList.Add(new WorkListItemViewModel
        {
            Id = 1,
            Title = "江城子 · 密州出猎",
            AuthorAndDynasty = "苏轼 · 宋",
            Content = "老夫聊发少年狂，左迁龙右擒苍"
            
        });
        workList.Add(new WorkListItemViewModel
        {
            Id = 1,
            Title = "观书有感",
            AuthorAndDynasty = "朱熹 · 宋",
            Content = "半亩方塘一鉴开，天光云影共徘徊"
            
        });
    }
    
    [RelayCommand]
    private void GetWorkList()
    {
        Task.Run(() => DoGetWorkList());
    }

    [RelayCommand]
    private void SearchWorkList()
    {
        Task.Run(() => DoGetWorkList());
    }
    
    public void DoGetWorks()
    {
        Task.Run(() => PoetryService.Instance.GetWorks());
    }

    public void DoGetWorkList()
    {
        WorkList.Clear();
        Console.WriteLine($"开始获取作品列表，关键字：\"{Keyword}\"");
        var workInfos = PoetryService.Instance.GetWorkList();
        foreach (var item in workInfos.Where(a =>
                     a.Title!.Contains(Keyword)
                     || a.Content!.Contains(Keyword)
                     || a.Author!.Contains(Keyword)))
        {
            WorkList.Add(new WorkListItemViewModel
            {
                Id = item.Id,
                Title = item.Title,
                AuthorAndDynasty = $"{item.Author} · {item.Dynasty}",
                Content = item.Content
            });
        }
    }

    [RelayCommand]
    private async Task BackToFirstView()
    {
        MobileNavigation.Pop();
        await Task.CompletedTask;
    }
}
