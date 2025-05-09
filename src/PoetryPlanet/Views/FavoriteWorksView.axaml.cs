using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class FavoriteWorksView : UserControl
{
    private FavoriteWorksViewModel vm;
    public FavoriteWorksView()
    {
        InitializeComponent();
        
        vm = new FavoriteWorksViewModel();
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