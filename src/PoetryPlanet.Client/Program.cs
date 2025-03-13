namespace PoetryPlanet.Client;

public static class Program
{
    /// <summary>
    /// 应用入口
    /// </summary>
    [STAThread]
    public static void Main(string[] args)
    {
        Container.Register(Locator.CurrentMutable, Locator.Current);
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    /// <summary>
    /// 构建应用，请勿删除，
    /// 设计时依赖该服务进行可视化渲染
    /// </summary>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    
   
}
