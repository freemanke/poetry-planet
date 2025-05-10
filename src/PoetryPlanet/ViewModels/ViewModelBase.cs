using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using PoetryPlanet.Services;
using PostSharp.Aspects.Advices;

namespace PoetryPlanet.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    protected readonly ILogger logger;
    protected PoetryService poetryService;

    protected ViewModelBase()
    {
        logger = App.GetRequiredService<ILoggerFactory>().CreateLogger(GetType().FullName!);
        poetryService = App.GetRequiredService<PoetryService>();
    }
}
