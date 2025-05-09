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

    private void InputElement_OnTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
        var vm = DataContext as WorkListItemViewModel;
        var workViewModel = Task.Run(() => vm?.CreateModelAsync()).Result;
        MobileNavigation.Push(new WorkView{DataContext = workViewModel});
    }
}