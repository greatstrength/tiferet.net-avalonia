using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_skeleton
/// <summary>
/// A pulsing placeholder shape for content loading states.
/// Variants: "rectangular" (default), "circular", "text".
/// </summary>
public class TiferetSkeleton : TemplatedControl
{
    // * attribute: variant_property
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TiferetSkeleton, string>(nameof(Variant), "rectangular");

    // *** properties

    // ** property: variant
    /// <summary>Shape variant: "rectangular", "circular", or "text".</summary>
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
