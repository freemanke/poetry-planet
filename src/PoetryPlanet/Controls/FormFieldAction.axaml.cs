using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Controls;

public partial class FormFieldAction : UserControl
{
    private ICommand? command;
    private object? commandParameter;
    
    public FormFieldAction()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
     
    public static readonly StyledProperty<string> TitleProperty = 
        AvaloniaProperty.Register<FormFieldSwitch, string>(nameof(Title), defaultValue: "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value );
    }
    
    public static readonly DirectProperty<FormFieldAction, ICommand?> CommandProperty =
        AvaloniaProperty.RegisterDirect<FormFieldAction, ICommand?>(
            nameof(Command), o => o.Command, (o, v) => o.Command = v);

    public static readonly DirectProperty<FormFieldAction, object?> CommandParameterProperty =
        AvaloniaProperty.RegisterDirect<FormFieldAction, object?>(
            nameof(CommandParameter), o => o.CommandParameter, (o, v) => o.CommandParameter = v);

    public ICommand? Command
    {
        get => command;
        set => SetAndRaise(CommandProperty, ref command, value);
    }

    public object? CommandParameter
    {
        get => commandParameter;
        set => SetAndRaise(CommandParameterProperty, ref commandParameter, value);
    }
}