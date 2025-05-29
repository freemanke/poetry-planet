using System;
using Avalonia.Controls;
using Avalonia.Input;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void FavoritesTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine($"{nameof(FavoritesTapped)}");
        var vm = App.GetRequiredService<FavoriteListViewModel>();
        vm.DoGetFavoriteWorks();
    }

    private void WorksTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine($"{nameof(WorksTapped)}");
        var vm = App.GetRequiredService<WorkListViewModel>();
        vm.DoGetWorkList();
    }
}