using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Views;

public partial class LogItemView : UserControl
{
    public LogItemView()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}