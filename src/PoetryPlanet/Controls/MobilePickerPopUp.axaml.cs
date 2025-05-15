using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace PoetryPlanet.Controls;

public partial class MobilePickerPopUp : UserControl
{
    public MobilePickerPopUp()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void DoneClick(object sender, RoutedEventArgs e)
    {
        InteractiveContainer.CloseDialog();
        var model = (MobilePickerPopUpViewModel)DataContext!;
        if (model.mobilePicker != null) model.mobilePicker.SelectedItem = model.SelectedItem;
    }
}

public class MobilePickerPopUpViewModel : ReactiveObject
{
    private string title = "";
    private string subtitle = "";
    private string selectedItem = "";
    private ObservableCollection<string> items = [];
    
    public MobilePicker? mobilePicker { get; set; }
    
    public ObservableCollection<string> Items
    {
        get => items;
        set => this.RaiseAndSetIfChanged(ref items, value);
    }

    public string SelectedItem
    {
        get => selectedItem;
        set => this.RaiseAndSetIfChanged(ref selectedItem, value);
    }

    public string Title
    {
        get => title;
        set => this.RaiseAndSetIfChanged(ref title, value);
    }

    public string SubTitle
    {
        get => subtitle;
        set => this.RaiseAndSetIfChanged(ref subtitle, value);
    }

   
}