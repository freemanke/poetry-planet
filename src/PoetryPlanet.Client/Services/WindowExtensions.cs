namespace PoetryPlanet.Client.Services;

public static class WindowExtensions
{
    public static T FindControl<T>(this Window window, string name) where T : Control
    {
        return window.FindNameScope()?.Find<T>(name)!;
    }
}