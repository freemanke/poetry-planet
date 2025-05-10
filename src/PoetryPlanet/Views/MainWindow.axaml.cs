using Avalonia.Controls;

namespace PoetryPlanet.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Height = 600;
        Width = Design.IsDesignMode ? 400 : 1920;
    }
}