using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class WorkListView : UserControl
{
    public WorkListView()
    {
        InitializeComponent();
        var vm = new WorkListViewModel();
        DataContext = vm;
       vm.DoLoadWorks();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}