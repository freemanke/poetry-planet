using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace PoetryPlanet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    private readonly AppSetting appSetting;
    [ObservableProperty] private bool isDark;

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