using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class CollectionView : UserControl
{
    public CollectionView()
    {
        InitializeComponent();
        DataContext = App.GetRequiredService<CollectionViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}