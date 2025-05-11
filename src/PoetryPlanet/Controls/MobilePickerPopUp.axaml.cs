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
        var model = (MobilePickerPopUpVM)DataContext!;
        if (model.mobilePicker != null) model.mobilePicker.SelectedItem = model.SelectedItem;
    }
}

public class MobilePickerPopUpVM: ReactiveObject
{
    private ObservableCollection<string> _items = new();

    public ObservableCollection<string> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }
    
    private string _selectedItem= "";

    public string SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }
    
    private string _title = "";

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    
    private string _subtitle = "";

    public string SubTitle
    {
        get => _subtitle;
        set => this.RaiseAndSetIfChanged(ref _subtitle, value);
    }

    public MobilePicker? mobilePicker { get; set; }
}