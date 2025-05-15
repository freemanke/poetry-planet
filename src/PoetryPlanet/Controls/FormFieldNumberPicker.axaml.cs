using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Controls;

public partial class FormFieldNumberPicker : UserControl
{
    private int value, maximum, minimum;

    public FormFieldNumberPicker()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static readonly DirectProperty<FormFieldNumberPicker, int> MaximumProperty =
        AvaloniaProperty.RegisterDirect<FormFieldNumberPicker, int>(
            nameof(Maximum), o => o.Maximum, (o, v) => o.Maximum = v, 0, BindingMode.TwoWay);

    public int Maximum
    {
        get => maximum;
        set => SetAndRaise(MaximumProperty, ref maximum, value);
    }


    public static readonly DirectProperty<FormFieldNumberPicker, int> MinimumProperty =
        AvaloniaProperty.RegisterDirect<FormFieldNumberPicker, int>(
            nameof(Minimum), o => o.Minimum, (o, v) => o.Minimum = v, 0, BindingMode.TwoWay);

    public int Minimum
    {
        get => minimum;
        set => SetAndRaise(MinimumProperty, ref minimum, value);
    }

    public static readonly DirectProperty<FormFieldNumberPicker, int> ValueProperty =
        AvaloniaProperty.RegisterDirect<FormFieldNumberPicker, int>(
            nameof(Value), o => o.Value, (o, v) => o.Value = v, 0, BindingMode.TwoWay);

    public int Value
    {
        get => value;
        set => SetAndRaise(ValueProperty, ref this.value, value);
    }

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<FormFieldSwitch, string>(nameof(Title), defaultValue: "Title");

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}