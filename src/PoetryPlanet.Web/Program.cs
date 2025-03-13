using System.Net;
using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PoetryPlanet.Web.Components;
using PoetryPlanet.Web.Components.Account;
using PoetryPlanet.Web.Data;
using Radzen;

namespace PoetryPlanet.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel((_, b) => { b.Listen(new IPEndPoint(IPAddress.Any, 5255)); });
        builder.Configuration.AddUserSecrets<Program>();
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.sss ";
        });
        
        RegisterServices(builder);
        RegisterDbMysql(builder);
       
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler(Consts.RouterError);
            app.UseHsts();
        }
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        app.MapAdditionalIdentityEndpoints();
        app.UseStatusCodePagesWithRedirects(Consts.RouterNotFound);
        StaticWebAssetsLoader.UseStaticWebAssets(app.Environment, app.Configuration);
        Initialize(app);
        app.Run();
    }

    public static void RegisterServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddBlazoredLocalStorage();
        services.AddRazorComponents().AddInteractiveServerComponents();
        services.AddRadzenComponents();
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
    }

    public static void RegisterDbMysql(WebApplicationBuilder builder, bool isTest = false)
    {
        var services = builder.Services;
        var config = builder.Configuration;
        var serverVersion = new MySqlServerVersion(new Version(5, 7, 44));
        var cb = new MySqlConnectionStringBuilder(config.GetConnectionString(Consts.DEFAULT_CONNECTION)!);
        var passwordFromUserSecrets = config[Consts.MYSQL_ROOT_PASSWORD];
        cb.Password = passwordFromUserSecrets;
        var pwdFromContainerEnv = Environment.GetEnvironmentVariable(Consts.MYSQL_ROOT_PASSWORD);
        if (string.IsNullOrEmpty(cb.Password)) cb.Password = pwdFromContainerEnv;

        // 如果是非 Development 环境且不是集成测试且是容器化运行时使用容器名称作为数据库服务器地址
        if (!builder.Environment.IsDevelopment() && !isTest && !string.IsNullOrEmpty(pwdFromContainerEnv)) 
            cb.Server = Consts.MYSQL_CONTAINER_NAME;
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(cb.ConnectionString, serverVersion).EnableDetailedErrors();
        });
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }
    
    public static void RegisterDbSqlite(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("DataSource=./data/poetry-planet.db;Cache=Shared"));
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }

    public static void RegisterDbInMemory(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb").EnableDetailedErrors();
        });
        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }

    private static void Initialize(WebApplication app)
    {
        var sp = ((IApplicationBuilder)app).ApplicationServices.CreateScope().ServiceProvider;
        var db = sp.GetRequiredService<ApplicationDbContext>();
        var logger = sp.GetRequiredService<ILogger<Program>>();
        var cb = new MySqlConnectionStringBuilder(db.Database.GetConnectionString()!);
        cb.Password = "*";
        
        logger.LogInformation("====================================================");
        logger.LogInformation($"当前环境: {app.Environment.EnvironmentName}");
        logger.LogInformation($"系统时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        logger.LogInformation($"数据库: {cb}");
        logger.LogInformation("=====================================================");
        logger.LogInformation("正在迁移数据库...");
        db.Database.Migrate();
        db.EnsuredInitialize(sp.GetRequiredService<IMapper>());
        logger.LogInformation("数据库迁移完成");
    }
}