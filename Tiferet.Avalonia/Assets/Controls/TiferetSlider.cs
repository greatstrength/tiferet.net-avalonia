using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_slider
/// <summary>
/// A themed range slider with configurable min/max, step, and optional value label.
/// </summary>
public class TiferetSlider : TemplatedControl
{
    // * attribute: value_property
    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<TiferetSlider, double>(nameof(Value));

    // * attribute: minimum_property
    public static readonly StyledProperty<double> MinimumProperty =
        AvaloniaProperty.Register<TiferetSlider, double>(nameof(Minimum), 0);

    // * attribute: maximum_property
    public static readonly StyledProperty<double> MaximumProperty =
        AvaloniaProperty.Register<TiferetSlider, double>(nameof(Maximum), 100);

    // * attribute: step_property
    public static readonly StyledProperty<double> StepProperty =
        AvaloniaProperty.Register<TiferetSlider, double>(nameof(Step), 1);

    // * attribute: show_label_property
    public static readonly StyledProperty<bool> ShowLabelProperty =
        AvaloniaProperty.Register<TiferetSlider, bool>(nameof(ShowLabel), false);

    // *** properties

    // ** property: value
    /// <summary>The current slider value.</summary>
    public double Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    // ** property: minimum
    /// <summary>The minimum value.</summary>
    public double Minimum
    {
        get => GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    // ** property: maximum
    /// <summary>The maximum value.</summary>
    public double Maximum
    {
        get => GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    // ** property: step
    /// <summary>The step increment.</summary>
    public double Step
    {
        get => GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    // ** property: show_label
    /// <summary>Whether to display the current value as a label.</summary>
    public bool ShowLabel
    {
        get => GetValue(ShowLabelProperty);
        set => SetValue(ShowLabelProperty, value);
    }
}
