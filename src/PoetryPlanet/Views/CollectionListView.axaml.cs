using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class CollectionListView : UserControl
{
    public CollectionListView()
    {
        InitializeComponent();
        var vm = App.GetRequiredService<CollectionListViewModel>();
        DataContext = vm;
        Task.Run(() => vm.DoGet());
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (DataContext is CollectionListViewModel vm) vm.DoGet();
    }
}