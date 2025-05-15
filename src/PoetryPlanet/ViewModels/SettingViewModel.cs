using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PoetryPlanet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    private readonly AppSetting appSetting;
    [ObservableProperty] private bool isDark;

    public SettingViewModel(AppSetting appSetting)
    {
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