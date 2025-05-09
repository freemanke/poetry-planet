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
        Console.WriteLine($"Tapped on Thread: {Thread.CurrentThread.ManagedThreadId}");
        var wlivm = DataContext as WorkListItemViewModel;
        var workViewModel = Task.Run(() => wlivm?.CreateModelAsync()).Result;
        MobileNavigation.Push(new WorkView{DataContext = workViewModel});
    }

    private void InputElement_OnPointerExited(object? sender, PointerEventArgs e)
    {
        Console.WriteLine($"pointerexited");
    }
}