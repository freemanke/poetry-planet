using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using CherylUI.Controls;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class NavigationView : UserControl
{
    public NavigationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TabTapped(object? sender, TappedEventArgs e)
    {
        var view = this.FindDescendantOfType<FavoriteWorksView>();
        if (view != null)
        {
            var vm = view.DataContext as FavoriteWorksViewModel;
            vm?.DoGetFavoriteWorks();
        }

    }
}