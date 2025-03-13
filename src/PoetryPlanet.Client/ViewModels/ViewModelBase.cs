using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PoetryPlanet.Client.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected ILogger logger { get; }

    protected ViewModelBase(ILoggerFactory loggerFactory)
    {
        logger = loggerFactory.CreateLogger(GetType().Name);
    }
}
