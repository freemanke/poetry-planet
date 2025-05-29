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

    private void TabFavoriteTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine("TabFavoriteTapped");
        var vm = App.GetRequiredService<FavoriteListViewModel>();
        vm.DoGetFavoriteWorks();
    }
}