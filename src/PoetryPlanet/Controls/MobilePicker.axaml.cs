using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace PoetryPlanet.Controls;

public partial class MobilePicker : UserControl
{
    private string title = "", subtitle = "", selectedItem = "";

    public MobilePicker()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public string SubTitle
    {
        get => subtitle;
        set => SetAndRaise(SubTitleProperty, ref subtitle, value);
    }

    public static readonly DirectProperty<MobilePicker, string> SubTitleProperty =
        AvaloniaProperty.RegisterDirect<MobilePicker, string>(
            nameof(SubTitle),
            o =>
            {
                Debug.Assert(o.SubTitle != null, "o.SubTitle != null");
                return o.SubTitle;
            },
            (o, v) => o.SubTitle = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    public string Title
    {
        get => title;
        set => SetAndRaise(TitleProperty, ref title, value);
    }

    public static readonly DirectProperty<MobilePicker, string> TitleProperty =
        AvaloniaProperty.RegisterDirect<MobilePicker, string>(
            nameof(Title),
            o => o.Title,
            (o, v) => o.Title = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);


    public string SelectedItem
    {
        get => selectedItem;
        set => SetAndRaise(SelectedItemProperty, ref selectedItem, value);
    }

    public static readonly DirectProperty<MobilePicker, string> SelectedItemProperty =
        AvaloniaProperty.RegisterDirect<MobilePicker, string>(
            nameof(SelectedItem),
            o => o.SelectedItem,
            (o, v) => o.SelectedItem = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);


    public static readonly StyledProperty<ObservableCollection<string>> ItemsProperty =
        AvaloniaProperty.Register<MobilePicker, ObservableCollection<string>>(nameof(Items),
            defaultValue: []);

    public ObservableCollection<string> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    private void OpenPopup(object sender, RoutedEventArgs e)
    {
        var control = new MobilePickerPopUp();

        var vm = (MobilePickerPopUpViewModel)control.DataContext!;
        vm.Items = Items;
        vm.SelectedItem = SelectedItem;
        vm.Title = Title;
        vm.SubTitle = SubTitle;
        vm.mobilePicker = this;


        control.Width = PopupWidth;
        control.FindControl<Border>("rootBorder")!.RenderTransform = PopupScale;

        InteractiveContainer.ShowDialog(control, true);
    }

    public static readonly StyledProperty<ScaleTransform> PopupScaleProperty =
        AvaloniaProperty.Register<MobilePicker, ScaleTransform>(nameof(MobilePicker),
            defaultValue: new ScaleTransform());

    public ScaleTransform PopupScale
    {
        get => GetValue(PopupScaleProperty);
        set => SetValue(PopupScaleProperty, value);
    }

    public static readonly StyledProperty<int> PopupWidthProperty =
        AvaloniaProperty.Register<MobilePicker, int>(nameof(MobilePicker), defaultValue: 300);

    public int PopupWidth
    {
        get => GetValue(PopupWidthProperty);
        set => SetValue(PopupWidthProperty, value);
    }
}