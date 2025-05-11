using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PoetryPlanet.Controls;


public partial class MobileNumberPickerPopup : UserControl
{
    public MobileNumberPicker _MobileNumberPicker;
    public int CurrentValue;
    private bool isScrolling;
    private Point StartingPosition;
    
    public MobileNumberPickerPopup()
    {
        _MobileNumberPicker = new MobileNumberPicker();
        InitializeComponent();
    }

    public MobileNumberPickerPopup(MobileNumberPicker _mobile)
    {
        _MobileNumberPicker = _mobile;
        InitializeComponent();
        SetTextValues(_mobile.Value);
        CurrentValue = _mobile.Value;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

  

    private new void PointerPressed(object sender, PointerPressedEventArgs e)
    {
        isScrolling = true;
        StartingPosition = e.GetPosition(this.FindControl<TextBlock>("CurrentValueText"));
    }

    private new void PointerReleased(object sender, PointerReleasedEventArgs e)
    {
        isScrolling = false;
        var difference = (StartingPosition.Y - e.GetPosition(this.FindControl<TextBlock>("CurrentValueText")).Y) / 5;

        _MobileNumberPicker.Value = (int)(CurrentValue + difference);
        CurrentValue = ((int)(CurrentValue + difference));
    }

    private new void PointerMoved(object sender, PointerEventArgs e)
    {
        if (isScrolling)
        {
            var difference = (StartingPosition.Y - e.GetPosition(this.FindControl<TextBlock>("CurrentValueText")).Y) /
                             5;
            var temporaryValue = (int)(CurrentValue + difference);

            if (_MobileNumberPicker != null && temporaryValue > _MobileNumberPicker.Maximum)
            {
                StartingPosition = e.GetPosition(this.FindControl<TextBlock>("CurrentValueText"));
                temporaryValue = _MobileNumberPicker.Maximum;
                CurrentValue = temporaryValue;
            }


            if (_MobileNumberPicker != null && temporaryValue < _MobileNumberPicker.Minimum)
            {
                temporaryValue = _MobileNumberPicker.Minimum;
                StartingPosition = e.GetPosition(this.FindControl<TextBlock>("CurrentValueText"));
                CurrentValue = temporaryValue;
            }
            SetTextValues(temporaryValue);
        }
    }
    
    private void SetTextValues(int temporaryValue)
    {
        this.FindControl<TextBlock>("CurrentValueText")!.Text = temporaryValue.ToString();
        this.FindControl<TextBlock>("CurrentValueTextMinus1")!.Text = temporaryValue - 1 < _MobileNumberPicker.Minimum
            ? ""
            : (temporaryValue - 1).ToString();
        this.FindControl<TextBlock>("CurrentValueTextPlus1")!.Text = temporaryValue + 1 > _MobileNumberPicker.Maximum
            ? ""
            : (temporaryValue + 1).ToString();
        this.FindControl<TextBlock>("CurrentValueTextPlus2")!.Text = temporaryValue + 2 > _MobileNumberPicker.Maximum
            ? ""
            : (temporaryValue + 2).ToString();

        this.FindControl<TextBlock>("CurrentValueTextMinus2")!.Text = temporaryValue - 2 < _MobileNumberPicker.Minimum
            ? ""
            : (temporaryValue - 2).ToString();
    }

    private void DoneClick(object sender, RoutedEventArgs e)
    {
        InteractiveContainer.CloseDialog();

    }
}