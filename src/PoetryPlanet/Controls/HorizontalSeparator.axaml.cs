using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Controls;

public partial class HorizontalSeparator : UserControl
{
    public HorizontalSeparator()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}