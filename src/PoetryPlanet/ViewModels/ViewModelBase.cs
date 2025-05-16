using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Controls;
using PoetryPlanet.Services;

namespace PoetryPlanet.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected readonly ILogger logger;
    protected readonly PoetryService poetryService;
    protected readonly AppSetting appSetting;

    protected ViewModelBase()
    {
        logger = App.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().FullName ?? "");
        poetryService = App.GetRequiredService<PoetryService>();
        appSetting = App.GetRequiredService<AppSetting>();
        if (GetType().Name != nameof(WorkListItemViewModel)
            && GetType().Name != nameof(CollectionListItemViewModel))
            logger.LogInformation("Create  view model {}", GetType().Name);
    }

    [RelayCommand]
    public void PreviousView()
    {
        MobileNavigation.Pop();
    }
}
