using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Services;
using PoetryPlanet.ViewModels;
using PoetryPlanet.Views;

namespace PoetryPlanet;

public class App : Application
{
    private static ServiceProvider? serviceProvider;
    
    public static T GetRequiredService<T>() where T : class
    {
        if(serviceProvider == null) ConfigServices();
        var t = serviceProvider?.GetRequiredService<T>()!;
        return t;
    }
    
    public static Control? GetRequiredService(Type type)
    {
        if(serviceProvider == null) ConfigServices();
        return serviceProvider?.GetRequiredService(type) as Control;
    }

    /// <summary>
    /// 依赖注册服务
    /// </summary>
    private static void ConfigServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton(LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Information).AddConsole()));
        services.AddLogging();
        services.AddSingleton<PoetryService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<WorkViewModel>();
        services.AddSingleton<WorkListViewModel>();
        services.AddSingleton<WorkListItemViewModel>();
        services.AddSingleton<FavoriteWorksViewModel>();
        services.AddSingleton<FavoriteWorkViewModel>();

        serviceProvider = services.BuildServiceProvider();
    }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var mainViewModel = GetRequiredService<MainViewModel>();
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