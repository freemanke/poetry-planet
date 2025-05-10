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
        vm = App.GetRequiredService<WorkListViewModel>();
        DataContext = vm;
        Task.Run(() => vm.DoGetWorks());
        Task.Run(() => vm.DoGetWorkList());
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void KeywordTextChanged(object? sender, TextChangedEventArgs e)
    {
        vm.DoGetWorkList();
    }
}