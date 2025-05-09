using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class FavoriteWorkView : UserControl
{
    public FavoriteWorkView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}