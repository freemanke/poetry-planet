using System;
using Avalonia.Input;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class WorkViewModel : ViewModelBase
{
    [ObservableProperty] private int id = 1001;
    [ObservableProperty] private string? title = "江城子 密州出猎";
    [ObservableProperty] private string? author = "苏轼";
    [ObservableProperty] private string? dynasty = "宋";
    [ObservableProperty] private string? content = "老夫聊发少年狂";
    [ObservableProperty] private string? intro = "简介";

    [RelayCommand]
    public void GoBack()
    {
        MobileNavigation.Pop();
    }
}