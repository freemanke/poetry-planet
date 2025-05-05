using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using CherylUI.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PoetryPlanet.Views;
using ReactiveUI;

namespace PoetryPlanet.ViewModels;

public partial class GuideFirstViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = "导出向导：第一步";

    [RelayCommand(CanExecute = nameof(CanAddItem))]
    public async Task Next()
    {
        MobileNavigation.Push(new GuideSecondView());
        await Task.CompletedTask;
    }

    private bool CanAddItem() => true;
}
