using Microsoft.Extensions.Logging;

namespace PoetryPlanet.Client.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private AppSetting appSetting;
    private string caption = "我的诗词星球";
    private string greeting = "Welcome to Avalonia!";
    private ThemeVariant themeVariant = ThemeVariant.Light;
    
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> HelpCommand { get; }
    
    /// <summary>
    /// 设计时使用的默认构造方法
    /// </summary>
    public MainWindowViewModel() 
        : this(new LoggerFactory(), new AppSetting())
    {
    }

    /// <summary>
    /// 依赖注入用的构造方法
    /// </summary>
    public MainWindowViewModel(ILoggerFactory loggerFactory, AppSetting appSetting)
    :base(loggerFactory)
    {
        this.appSetting = appSetting;
        SaveCommand = ReactiveCommand.Create(() =>
        {
            ThemeVariant = ThemeVariant == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
            appSetting.Save();
            logger.LogInformation($"配置已保存到 {Serializer.Serialize(appSetting)}");
           
        });
        HelpCommand = ReactiveCommand.Create(() =>
        {
            Console.WriteLine("点击帮助菜单");
        });
    }
    
    public string Title => Constants.AppTitle;

    public ThemeVariant ThemeVariant
    {
        get => themeVariant;
        set => this.RaiseAndSetIfChanged(ref themeVariant, value);
    }

    public string Caption
    {
        get => caption;
        set => this.RaiseAndSetIfChanged(ref caption, value);
    }
    
    public string Greeting
    {
        get => greeting;
        set => this.RaiseAndSetIfChanged(ref greeting, value);
    }

    public AppSetting Setting
    {
        get => appSetting;
        set => this.RaiseAndSetIfChanged(ref appSetting, value);
    }
}
