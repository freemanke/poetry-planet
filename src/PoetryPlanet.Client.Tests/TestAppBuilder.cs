using Avalonia.Headless;
using PoetryPlanet.Client.Tests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace PoetryPlanet.Client.Tests;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}