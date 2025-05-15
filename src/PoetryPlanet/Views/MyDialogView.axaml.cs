using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PoetryPlanet.Controls;

namespace PoetryPlanet.Views;

public partial class MyDialogView : UserControl
{
    public MyDialogView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        InteractiveContainer.CloseDialog();
    }
}