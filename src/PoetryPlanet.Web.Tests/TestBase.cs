namespace PoetryPlanet.Web.Tests;

public class TestBase
{
    public WebApplicationBuilder? Builder { get; set; }
    protected IServiceProvider? ServiceProvider { get; set; }

    protected T GetRequiredService<T>() where T : notnull
        => ServiceProvider!.GetRequiredService<T>();

    public T GetHostedService<T>() where T : class
    {
        var services = ServiceProvider!.GetServices<IHostedService>();
        return services.Where(service => service is T).Cast<T>().FirstOrDefault()!;
    }

    protected static void Log(string? message)
    {
        TestContext.Out.WriteLine(message);
    }
}
