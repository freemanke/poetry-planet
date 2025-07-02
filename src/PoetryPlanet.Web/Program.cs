using System.Net;
using System.Reflection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using PoetryPlanet.Data;
using PoetryPlanet.Data.Repositories;
using PoetryPlanet.Services;
using PoetryPlanet.Web.Components;
using PoetryPlanet.Web.Components.Account;
using PoetryPlanet.Web.Controllers;
using PoetryPlanet.Web.Services;
using Radzen;

namespace PoetryPlanet.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        RegisterServices(builder);
        RegisterDbMysql(builder);
        
        // 注册文档框架组件
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();
        app.UseExceptionHandler(Consts.RouterError);
        app.UseHsts();
        app.MapControllers();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.UseSwagger(c => { c.RouteTemplate = "/api/swagger/{documentname}/swagger.json"; });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/api/swagger/v1/swagger.json", "授权管理客户端接口");
            c.RoutePrefix = "api/swagger"; // 通过该路由访问 Swagger UI
        });
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
        builder.WebHost.ConfigureKestrel((_, b) => { b.Listen(new IPEndPoint(IPAddress.Any, 5255)); });
        builder.Configuration.AddUserSecrets<Program>();
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.sss ";
        });
        services.AddBlazoredLocalStorage();
        services.AddRazorComponents().AddInteractiveServerComponents();
        services.AddRadzenComponents();
        services.AddControllers();
        services.AddScoped<WorkController>();
        services.AddScoped<WorkListController>();
        services.AddScoped<CollectionController>();
        services.AddCascadingAuthenticationState();
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<WorkService>();
        services.AddScoped<DebugService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        services.AddSingleton<ChatService>();
    }

    public static void RegisterDbMysql(WebApplicationBuilder builder, bool isTest = false)
    {
        var services = builder.Services;
        var config = builder.Configuration;
        var serverVersion = new MySqlServerVersion(new Version(5, 7, 44));
        var cb = new MySqlConnectionStringBuilder(config.GetConnectionString(Consts.DEFAULT_CONNECTION) ?? "");
        var passwordFromUserSecrets = config[Consts.MYSQL_ROOT_PASSWORD];
        cb.Password = passwordFromUserSecrets;
        var pwdFromContainerEnv = Environment.GetEnvironmentVariable(Consts.MYSQL_ROOT_PASSWORD);
        if (string.IsNullOrEmpty(cb.Password)) cb.Password = pwdFromContainerEnv;
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(cb.ConnectionString, serverVersion);
        });
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }
    
    public static void RegisterDbSqlite(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("DataSource=/Users/freeman/Downloads/poetry-planet.sqlite;Cache=Shared"));
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
        var cb = new MySqlConnectionStringBuilder(db.Database.GetConnectionString()??"");
        cb.Password = "*";
        
        logger.LogInformation("====================================================");
        logger.LogInformation($"当前环境: {app.Environment.EnvironmentName}");
        logger.LogInformation($"系统时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        logger.LogInformation($"数据库: {cb}");
        logger.LogInformation("=====================================================");
        Task.Run(() =>
        {
            var interval = TimeSpan.FromSeconds(10);
            logger.LogInformation("正在迁移数据库...");
            while (true)
            {
                try
                {
                    db.Database.Migrate();
                    db.EnsuredInitialize();
                    break;
                }
                catch (Exception)
                {
                    logger.LogError($"迁移数据库错误，{interval} 后重试");
                }
                
                Thread.Sleep(interval);
            }

            logger.LogInformation("数据库迁移完成");
        });
    }
}