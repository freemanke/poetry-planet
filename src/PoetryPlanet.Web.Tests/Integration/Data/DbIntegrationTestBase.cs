using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Build.Framework.ILogger;

#pragma warning disable CS8618
namespace PoetryPlanet.Web.Tests.Integration.Data;

public abstract class DbIntegrationTestBase : TestBase
{
    protected ApplicationDbContext db;

    protected DbIntegrationTestBase(string environmentName)
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            { EnvironmentName = environmentName });
        builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json")
            .AddUserSecrets<DbContextTestProduction>().Build();
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.IncludeScopes = true;
            options.TimestampFormat = "HH:mm:ss.sss ";
        });
        
        Program.RegisterServices(builder);
        Program.RegisterDbMysql(builder, true);
        ServiceProvider = builder.Services.BuildServiceProvider();

        db = GetRequiredService<ApplicationDbContext>();
        Assert.That(GetRequiredService<IHostEnvironment>().EnvironmentName, Is.EqualTo(environmentName));
        Assert.That(db.Database.ProviderName, Is.EqualTo("Pomelo.EntityFrameworkCore.MySql"));
    }

    public virtual void EnsuredInitialize()
    {
        Log($"数据库信息：{db.Database.GetConnectionString()!.Substring(0,49)}...");
        Log("正在迁移数据库...");
        db.Database.Migrate();
        Log("数据库迁移完成");
        db.EnsuredInitialize(GetRequiredService<IMapper>());
        Assert.That(db.Authors.Count(), Is.Not.EqualTo(0));
        
        var userFavorite = new UserFavoriteWork { WorkId = 100, Username = "freemanke" };
        db.UserFavoriteWorks.Add(userFavorite);
        db.SaveChanges();
        db.UserFavoriteWorks.Remove(userFavorite);
        db.SaveChanges();
    }
}