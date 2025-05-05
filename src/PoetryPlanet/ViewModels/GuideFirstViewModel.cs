using System;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
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

    public ICommand NextCommand { get; } = new RelayCommand(() =>
    {
        MobileNavigation.Push(new GuideSecondView());
        Console.WriteLine("next");
    });
}
