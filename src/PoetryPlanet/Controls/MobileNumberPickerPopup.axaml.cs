using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Controls;

public partial class MobileNumberPickerPopup : UserControl
{
    public readonly MobileNumberPicker picker;
    public int CurrentValue;
    private bool isScrolling;
    private Point StartingPosition;
    
    public MobileNumberPickerPopup()
    {
        picker = new MobileNumberPicker();
        InitializeComponent();
    }

    public MobileNumberPickerPopup(MobileNumberPicker picker)
    {
        this.picker = picker;
        InitializeComponent();
        SetTextValues(picker.Value);
        CurrentValue = picker.Value;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private new void PointerPressed(object sender, PointerPressedEventArgs e)
    {
        isScrolling = true;
        StartingPosition = e.GetPosition(FindTextBlock());
    }

    private TextBlock? FindTextBlock(string controlName = "CurrentValueText")
    {
        return this.FindControl<TextBlock>(controlName);
    }

    private new void PointerReleased(object sender, PointerReleasedEventArgs e)
    {
        isScrolling = false;
        var difference = (StartingPosition.Y - e.GetPosition(FindTextBlock()).Y) / 5;

        picker.Value = (int)(CurrentValue + difference);
        CurrentValue = ((int)(CurrentValue + difference));
    }

    private new void PointerMoved(object sender, PointerEventArgs e)
    {
        if (isScrolling)
        {
            var diff = (StartingPosition.Y - e.GetPosition(FindTextBlock()).Y) / 5;
            var value = (int)(CurrentValue + diff);
            if (value > picker.Maximum)
            {
                StartingPosition = e.GetPosition(FindTextBlock());
                value = picker.Maximum;
                CurrentValue = value;
            }

            if (value < picker.Minimum)
            {
                value = picker.Minimum;
                StartingPosition = e.GetPosition(FindTextBlock());
                CurrentValue = value;
            }

            SetTextValues(value);
        }
    }

    private void SetTextValues(int value)
    {
        FindTextBlock()!.Text = value.ToString();
        FindTextBlock("CurrentValueTextMinus1")!.Text = value - 1 < picker.Minimum ? "" : (value - 1).ToString();
        FindTextBlock("CurrentValueTextPlus1")!.Text = value + 1 > picker.Maximum ? "" : (value + 1).ToString();
        FindTextBlock("CurrentValueTextPlus2")!.Text = value + 2 > picker.Maximum ? "" : (value + 2).ToString();
        FindTextBlock("CurrentValueTextMinus2")!.Text = value - 2 < picker.Minimum ? "" : (value - 2).ToString();
    }

    private void DoneClick(object sender, RoutedEventArgs e)
    {
        InteractiveContainer.CloseDialog();
    }
}