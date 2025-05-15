using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Controls;
using PoetryPlanet.Services;
using PostSharp.Aspects.Advices;

namespace PoetryPlanet.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected readonly ILogger logger;
    protected readonly PoetryService poetryService;

    protected ViewModelBase()
    {
        logger = App.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().FullName!);
        poetryService = App.GetRequiredService<PoetryService>();
        if (GetType().Name != nameof(WorkListItemViewModel))
            logger.LogInformation($"创建视图模型 {GetType().Name}");
    }
    
    [RelayCommand]
    public void PreviousView()
    {
        MobileNavigation.Pop();
    } 
}
