using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using PoetryPlanet.ViewModels;

namespace PoetryPlanet;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null) return null;
        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type == null) return new TextBlock { Text = "没有找到视图：" + name };
        var view = App.GetRequiredService(type);
        return view;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}