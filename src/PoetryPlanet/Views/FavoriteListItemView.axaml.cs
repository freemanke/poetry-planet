using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PoetryPlanet.Controls;
using PoetryPlanet.ViewModels;
using MobileNavigation = PoetryPlanet.Controls.MobileNavigation;

namespace PoetryPlanet.Views;

public partial class FavoriteListItemView : UserControl
{
    public FavoriteListItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void WorkListItemTapped(object? sender, TappedEventArgs e)
    {
        var vm = DataContext as FavoriteListItemViewModel;
        var workViewModel = vm?.CreateWorkViewModel();
        MobileNavigation.Push(new WorkView{DataContext = workViewModel});
    }
}