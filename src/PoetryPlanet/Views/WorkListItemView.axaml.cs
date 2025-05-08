using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
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
        var vm = DataContext as WorkListItemViewModel;
        vm?.OpenWorkView();

    }
}