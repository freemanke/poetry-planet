using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CherylUI.Controls;
using PoetryPlanet.Dtos;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class WorkListView : UserControl
{
    private WorkListViewModel vm;
    public WorkListView()
    {
        InitializeComponent();
        vm = new WorkListViewModel();
        DataContext = vm;
        Task.Run(() => vm.DoGetWorks());
        Task.Run(() => vm.DoGetWorkList());
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        vm.DoGetWorkList();
    }

    private void InputElement_OnTapped(object? sender, TappedEventArgs e)
    {
       Console.WriteLine($"tapped");
    }
}