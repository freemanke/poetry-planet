using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using AutoMapper;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Data;
using PoetryPlanet.Services;
using PoetryPlanet.ViewModels;
using PoetryPlanet.Views;

namespace PoetryPlanet;

public class App : Application
{
    private static ServiceProvider? serviceProvider;
    public static string DatabaseFilePath => Path.Combine(AppSetting.ConfigRootPath, "poetry-planet.sqlite");
    
    public static T GetRequiredService<T>() where T : class
    {
        if(serviceProvider == null) ConfigServices();
        if (serviceProvider == null) throw new NullReferenceException(nameof(serviceProvider));
        return serviceProvider.GetRequiredService<T>();
    }
    
    public static Control? GetRequiredService(Type type)
    {
        if(serviceProvider == null) ConfigServices();
        return serviceProvider?.GetRequiredService(type) as Control;
    }

    public static void ChangeTheme(bool isDark)
    {
        if(Current == null) return;
        Current.RequestedThemeVariant = isDark ? ThemeVariant.Dark : ThemeVariant.Light;
    }

    /// <summary>
    /// 依赖注册服务
    /// </summary>
    private static void ConfigServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton(LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information).AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff ";
            });
            builder.AddProvider(new CustomFileLoggerProvider(new StreamWriter(AppSetting.LogFilePath, append: true)));
        }));
        services.AddLogging();
        services.AddSingleton(AppSetting.Load());
        services.AddSingleton<PoetryService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MineViewModel>();
        services.AddSingleton<SettingViewModel>();
        services.AddSingleton<WorkListViewModel>();
        services.AddSingleton<WorkListItemViewModel>();
        services.AddSingleton<FavoriteWorksViewModel>();
        services.AddSingleton<FavoriteWorkViewModel>();
        services.AddSingleton<CollectionListViewModel>();
        services.AddTransient<CollectionListItemViewModel>();
        services.AddTransient<WorkViewModel>();
        services.AddTransient<CollectionViewModel>();
        
        // 注册 SQLite 数据库组件
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite($"DataSource={DatabaseFilePath};Cache=Shared"));
        //services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
          //  .AddEntityFrameworkStores<ApplicationDbContext>();
        serviceProvider = services.BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var mainViewModel = GetRequiredService<MainViewModel>();
        var appSetting = GetRequiredService<AppSetting>();
        ChangeTheme(appSetting.IsDark);
        var db = GetRequiredService<ApplicationDbContext>();

        try
        {
            db.EnsuredInitialize();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow { DataContext = mainViewModel };
                break;
            case ISingleViewApplicationLifetime mobile:
                mobile.MainView = new MainView { DataContext = mainViewModel };
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
    /// More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
    /// </summary>
    private static void DisableAvaloniaDataAnnotationValidation()
    {
        var plugins = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();
        foreach (var plugin in plugins)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}