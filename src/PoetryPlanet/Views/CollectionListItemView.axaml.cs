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

public partial class CollectionListItemView : UserControl
{
    public CollectionListItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ItemTapped(object? sender, TappedEventArgs e)
    {
        var currentVm = DataContext as CollectionListItemViewModel;
        var vm = new CollectionListItemViewModel();
        
        
        MobileNavigation.Push(new CollectionView(){DataContext = vm});
    }
}