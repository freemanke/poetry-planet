using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using PoetryPlanet.Services;
using PoetryPlanet.Views;

namespace PoetryPlanet.ViewModels;

public partial class WorkListItemViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? author = "苏轼";
    [ObservableProperty] private string? dynasty = "宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂";

    public void OpenWorkView()
    {
        var work = new PoetryService().GetWork(Id);
        var vm = new WorkViewModel
        {
            Id = work.Id,
            Title = work.Title,
            Author = work.Author,
            Content = work.Content,
            Dynasty = work.Dynasty,
            Intro = work.Intro,
        };
        
        MobileNavigation.Push(new WorkView{DataContext = vm});
    }
}