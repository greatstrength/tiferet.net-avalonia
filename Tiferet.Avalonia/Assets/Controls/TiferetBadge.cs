using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_badge
/// <summary>
/// A compact status indicator pill.
/// Variants: "default", "success", "warning", "error", "info".
/// </summary>
public class TiferetBadge : TemplatedControl
{
    // * attribute: label_property
    public static readonly StyledProperty<string?> LabelProperty =
        AvaloniaProperty.Register<TiferetBadge, string?>(nameof(Label));

    // * attribute: variant_property
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TiferetBadge, string>(nameof(Variant), "default");

    // *** properties

    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>Semantic variant: "default", "success", "warning", "error", "info".</summary>
    public string Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    // *** methods

    // ** method: on_property_changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == VariantProperty)
        {
            if (change.OldValue is string old) PseudoClasses.Remove($":{old}");
            if (change.NewValue is string nv) PseudoClasses.Add($":{nv}");
        }
    }

    // ** method: on_apply_template
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        PseudoClasses.Add($":{Variant}");
    }
}
