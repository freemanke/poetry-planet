using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Views;

public partial class NavigationView : UserControl
{
    public NavigationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}