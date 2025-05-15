using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace PoetryPlanet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    private readonly AppSetting appSetting;
    [ObservableProperty] private bool isDark;

    /// <summary>
    /// 默认构造方法，用于设计模式
    /// </summary>
    public SettingViewModel() : this(new AppSetting())
    {
        logger.LogInformation("使用默认构造方法");
    }

    public SettingViewModel(AppSetting appSetting)
    {
        logger.LogInformation($"使用带参数构造方法");
        this.appSetting = appSetting;
        IsDark = appSetting.IsDark;
    }

    [RelayCommand]
    private void ChangeTheme()
    {
        appSetting.IsDark = IsDark;
        appSetting.Save();
        App.ChangeTheme(appSetting.IsDark);
    }
}