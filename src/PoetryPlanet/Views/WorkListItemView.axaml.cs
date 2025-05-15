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

public partial class WorkListItemView : UserControl
{
    public WorkListItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void WorkListItemTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine($"当前线程 {Environment.CurrentManagedThreadId}");
        var vm = DataContext as WorkListItemViewModel;
        var workViewModel = vm?.CreateWorkViewModel();
        MobileNavigation.Push(new WorkView{DataContext = workViewModel});
    }
}