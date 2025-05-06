using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Views;

public partial class WorksView : UserControl
{
    public WorksView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}