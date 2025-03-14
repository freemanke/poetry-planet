using System.Runtime.CompilerServices;

namespace PoetryPlanet.Web.Tests.Unit;

public class UnitTestBase : TestBase
{
    public UnitTestBase()
    {
        var environmentName = Environments.Development;
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = environmentName });
        builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json").AddUserSecrets<UnitTestBase>().Build();
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.IncludeScopes = true;
            options.TimestampFormat = "HH:mm:ss.sss ";
        });

        Program.RegisterServices(builder);
        Program.RegisterDbInMemory(builder);
        ServiceProvider = builder.Services.BuildServiceProvider();
        db = GetRequiredService<ApplicationDbContext>();
        Assert.That(db.Database.ProviderName, Is.EqualTo("Microsoft.EntityFrameworkCore.InMemory"));
    }

    [Test]
    public void NameOf()
    {
        var name = $"{typeof(UnitOfWork).Namespace}.*";
        Assert.That(name, Is.EqualTo("PoetryPlanet.Web.Data.Repositories.*"));
    }

    [Test]
    public void ReturnNullable()
    {
        var method = typeof(Repository<Dynasty>).GetMethod(nameof(Repository<Dynasty>.FindAsync));
        var finds = method!.ReturnTypeCustomAttributes.GetCustomAttributes(typeof(NullableAttribute), true);
        Assert.That(finds, Has.Length.EqualTo(1));
    }

    [Test]
    public void ReturnUnNullable()
    {
        var method = typeof(Repository<Dynasty>).GetMethod(nameof(Repository<Dynasty>.SelectToListAsync));
        var finds = method!.ReturnTypeCustomAttributes.GetCustomAttributes(typeof(NullableAttribute), true);
        Assert.That(finds, Is.Empty);
    }
}