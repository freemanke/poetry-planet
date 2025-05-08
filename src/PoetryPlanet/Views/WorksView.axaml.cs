using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class WorksView : UserControl
{
    public WorksView()
    {
        InitializeComponent();
        DataContext = new WorksViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}