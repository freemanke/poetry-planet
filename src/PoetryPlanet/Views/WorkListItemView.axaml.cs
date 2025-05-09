using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CherylUI.Controls;
using PoetryPlanet.ViewModels;

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
        var workViewModel = vm?.CreateViewModel();
        MobileNavigation.Push(new WorkView{DataContext = workViewModel});
    }
}