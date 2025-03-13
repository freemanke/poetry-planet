using Microsoft.Extensions.Logging;
using PoetryPlanet.Client.ViewModels;
using PoetryPlanet.Client.Views;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PoetryPlanet.Client;

/// <summary>
/// 依赖注入容器
/// </summary>
public static class Container
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        // 注册服务
        services.RegisterLazySingleton(() => LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("PoetryPlanet", LogLevel.Debug)
                .AddConsole();
        }));
        var appSetting = AppSetting.Load(Constants.SettingFilePath);
        var logger = GetRequiredService<ILoggerFactory>().CreateLogger(nameof(AppSetting));
        logger.LogInformation($"配置加载成功 {Serializer.Serialize(appSetting)}");
        
        services.RegisterLazySingleton(() => appSetting);
        services.RegisterLazySingleton(() => new MainWindow
            { DataContext = GetRequiredService<MainWindowViewModel>() });
        services.RegisterLazySingleton(() => new MainWindowViewModel(
            GetRequiredService<ILoggerFactory>(),
            GetRequiredService<AppSetting>()));
    }

    /// <summary>
    /// 获取服务
    /// </summary>
    public static TService GetRequiredService<TService>()
    {
        var service = Locator.Current.GetService<TService>();
        if (service is null) throw new InvalidOperationException($"服务解析服务 {typeof(TService)}");

        return service;
    }
}