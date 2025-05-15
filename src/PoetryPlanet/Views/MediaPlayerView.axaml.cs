using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class MediaPlayerView : UserControl
{
    public MediaPlayerView()
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = new MediaPlayerViewModel();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as MediaPlayerViewModel;
        vm?.Play();
    }
}