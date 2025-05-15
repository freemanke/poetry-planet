using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace PoetryPlanet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    [ObservableProperty] private bool isDark;

    public SettingViewModel()
    {
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