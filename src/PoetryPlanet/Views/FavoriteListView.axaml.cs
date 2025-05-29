using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class FavoriteListView : UserControl
{
    public FavoriteListView()
    {
        InitializeComponent();
        DataContext = App.GetRequiredService<FavoriteListViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}