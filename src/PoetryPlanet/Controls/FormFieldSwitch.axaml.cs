using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Controls;

public partial class FormFieldSwitch : UserControl
{
    private bool isChecked;
    private ICommand? command;


    public FormFieldSwitch()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static readonly DirectProperty<FormFieldSwitch, bool> IsCheckedProperty =
        AvaloniaProperty.RegisterDirect<FormFieldSwitch, bool>(
            nameof(IsChecked), o => o.IsChecked, (o, v) => o.IsChecked = v, false, BindingMode.TwoWay);

    public bool IsChecked
    {
        get => isChecked;
        set => SetAndRaise(IsCheckedProperty, ref isChecked, value);
    }

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<FormFieldSwitch, string>(nameof(Title), defaultValue: "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DirectProperty<FormFieldSwitch, ICommand> CommandProperty =
        AvaloniaProperty.RegisterDirect<FormFieldSwitch, ICommand>(
            nameof(Command), o => o.Command!, (o, v) => o.Command = v);

    public ICommand? Command
    {
        get => command;
        set => SetAndRaise(CommandProperty!, ref command, value);
    }
}