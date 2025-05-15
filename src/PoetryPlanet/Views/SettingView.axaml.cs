using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet.Views;

public partial class SettingView : UserControl
{
    public SettingView()
    {
        InitializeComponent();
        DataContext = App.GetRequiredService<SettingViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}