using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class FavoriteListView : UserControl
{
    public FavoriteListView()
    {
        InitializeComponent();

        var vm = App.GetRequiredService<FavoriteListViewModel>();
        DataContext = vm;
        Task.Run(() => vm.DoGetFavoriteWorks());
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void InputElement_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        Console.WriteLine("getfocus");
    }
}