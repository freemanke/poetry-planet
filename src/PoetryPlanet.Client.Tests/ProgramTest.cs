namespace PoetryPlanet.Client.Tests;

public class BootstrapperTest : UnitTestBase
{
    [AvaloniaTest]
    public void AllServiceShouldBeRegistered()
    {
        var appSetting = GetRequiredService<AppSetting>();
        Assert.That(appSetting, Is.Not.Null);
        
        var mainWindow = GetRequiredService<MainWindow>();
        Assert.That(mainWindow, Is.Not.Null);
        
        var mainWindowViewModel = GetRequiredService<MainWindowViewModel>();
        Assert.That(mainWindowViewModel, Is.Not.Null);
    }
}