#pragma warning disable CS8618 
namespace PoetryPlanet.Web.Tests;

public abstract class TestBase
{
    protected ApplicationDbContext db { get; set; }
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
