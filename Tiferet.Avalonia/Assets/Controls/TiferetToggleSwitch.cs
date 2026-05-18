using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_toggle_switch
/// <summary>
/// A themed toggle switch with optional on/off labels.
/// Sizes: "sm", "md" (default).
/// </summary>
public class TiferetToggleSwitch : TemplatedControl
{
    // * attribute: is_checked_property
    public static readonly StyledProperty<bool> IsCheckedProperty =
        AvaloniaProperty.Register<TiferetToggleSwitch, bool>(nameof(IsChecked));

    // * attribute: on_label_property
    public static readonly StyledProperty<string?> OnLabelProperty =
        AvaloniaProperty.Register<TiferetToggleSwitch, string?>(nameof(OnLabel));

    // * attribute: off_label_property
    public static readonly StyledProperty<string?> OffLabelProperty =
        AvaloniaProperty.Register<TiferetToggleSwitch, string?>(nameof(OffLabel));

    // * attribute: size_property
    public static readonly StyledProperty<string> SizeProperty =
        AvaloniaProperty.Register<TiferetToggleSwitch, string>(nameof(Size), "md");

    // *** properties

    // ** property: is_checked
    /// <summary>Whether the toggle is in the on state.</summary>
    public bool IsChecked
    {
        get => GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    // ** property: on_label
    /// <summary>Label displayed when the toggle is on.</summary>
    public string? OnLabel
    {
        get => GetValue(OnLabelProperty);
        set => SetValue(OnLabelProperty, value);
    }

    // ** property: off_label
    /// <summary>Label displayed when the toggle is off.</summary>
    public string? OffLabel
    {
        get => GetValue(OffLabelProperty);
        set => SetValue(OffLabelProperty, value);
    }

    // ** property: size
    /// <summary>Size: "sm" or "md".</summary>
    public string Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // *** methods

    // ** method: on_property_changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IsCheckedProperty)
        {
            var isChecked = (bool)change.NewValue!;
            if (isChecked) PseudoClasses.Add(":checked");
            else PseudoClasses.Remove(":checked");
        }
        else if (change.Property == SizeProperty)
        {
            if (change.OldValue is string old) PseudoClasses.Remove($":{old}");
            if (change.NewValue is string nv) PseudoClasses.Add($":{nv}");
        }
    }

    // ** method: on_apply_template
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // Set initial pseudo-classes.
        if (IsChecked) PseudoClasses.Add(":checked");
        PseudoClasses.Add($":{Size}");
    }
}
