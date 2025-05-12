using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace PoetryPlanet.Controls;

public partial class GlassCard : UserControl
{
    public GlassCard()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public new static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
        AvaloniaProperty.Register<GlassCard, CornerRadius>(nameof(CornerRadius), new CornerRadius(20));

    
    public new static readonly StyledProperty<IBrush> BorderBrushProperty =
        AvaloniaProperty.Register<GlassCard, IBrush>(nameof(BorderBrush), Brushes.Black);

    public new IBrush BorderBrush
    {
        get => GetValue(BorderBrushProperty);
        set => SetValue(BorderBrushProperty, value);
    }
    
    public new CornerRadius CornerRadius
    {
        get => GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    public new static readonly StyledProperty<Thickness> BorderThicknessProperty =
        AvaloniaProperty.Register<GlassCard, Thickness>(nameof(BorderThickness), new Thickness(1));

    public new Thickness BorderThickness
    {
        get => GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }
}