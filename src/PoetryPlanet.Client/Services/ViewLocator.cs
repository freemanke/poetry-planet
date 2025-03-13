using PoetryPlanet.Client.ViewModels;

namespace PoetryPlanet.Client.Services;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null) return null;
        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);
        if (type != null) return (Control)Activator.CreateInstance(type)!;
        return new TextBlock { Text = "未找到控件：" + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
