using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class WorkListView : UserControl
{
    private WorkListViewModel vm;
    public WorkListView()
    {
        InitializeComponent();
        vm = new WorkListViewModel();
        DataContext = vm;
        Task.Run(() => vm.DoGetWorks());
        Task.Run(() => vm.DoGetWorkList());
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        vm.DoGetWorkList();
    }
}