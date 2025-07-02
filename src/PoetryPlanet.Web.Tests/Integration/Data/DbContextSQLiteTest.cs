namespace PoetryPlanet.Web.Tests.Integration.Data;

public class DbContextSQLiteTest
{
    [Test]
    public void EnsuredInitialize()
    {
        var environmentName = "Development";
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            { EnvironmentName = environmentName });
        builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json")
            .AddUserSecrets<ContextTestProduction>().Build();
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.IncludeScopes = true;
            options.TimestampFormat = "HH:mm:ss.sss ";
        });
        
        Program.RegisterServices(builder);
        Program.RegisterDbSqlite(builder);
        var ServiceProvider = builder.Services.BuildServiceProvider();

        var db = ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Assert.That(ServiceProvider.GetRequiredService<IHostEnvironment>().EnvironmentName, Is.EqualTo(environmentName));
        Assert.That(db.Database.ProviderName, Is.EqualTo("Microsoft.EntityFrameworkCore.Sqlite"));
        db.Database.EnsureCreated();
        db.EnsuredInitialize();
    }
}