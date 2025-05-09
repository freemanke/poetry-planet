using System;
using System.Threading;
using System.Threading.Tasks;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Services;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class WorkListItemViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? authorAndDynasty = "苏轼 · 宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂，左迁龙右擒苍";

    public async Task<WorkViewModel> CreateModelAsync()
    {
        Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
        var work = await Task.Run(() => PoetryService.Instance.GetWork(Id));
        var vm = new WorkViewModel
        {
            Id = work.Id,
            Title = work.Title,
            Author = work.Author,
            Content = work.Content,
            Dynasty = work.Dynasty,
            Intro = work.Intro,
        };
        
        return vm;
    }

    [RelayCommand]
    private void Favorite()
    {
        Console.WriteLine($"Favorite");
    }
}