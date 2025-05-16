using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.VisualTree;
using Material.Styles.Themes;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent(); 
    }
    
    private void TabFavoriteTapped(object? sender, TappedEventArgs e)
    {
        var view = this.FindDescendantOfType<FavoriteWorksView>();
        if (view != null)
        {
            var vm = view.DataContext as FavoriteWorksViewModel;
            vm?.DoGetFavoriteWorks();
        }
    }
}