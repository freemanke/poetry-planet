namespace PoetryPlanet.Client.Tests;

public class TestBase
{
    protected TestBase()
    {
        Container.Register(Locator.CurrentMutable, Locator.Current);
    }

    protected T GetRequiredService<T>()
    {
        return Container.GetRequiredService<T>();
    }
}

public class UnitTestBase: TestBase;