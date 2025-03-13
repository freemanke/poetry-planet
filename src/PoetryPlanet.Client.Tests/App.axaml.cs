using Avalonia;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Client.Tests;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}