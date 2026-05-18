using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_text_box
/// <summary>
/// A themed text input with variant support.
/// Variants: "outlined" (default), "filled".
/// </summary>
public class TiferetTextBox : TemplatedControl
{
    // * attribute: text_property
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<TiferetTextBox, string?>(nameof(Text));

    // * attribute: placeholder_property
    public static readonly StyledProperty<string?> PlaceholderProperty =
        AvaloniaProperty.Register<TiferetTextBox, string?>(nameof(Placeholder));

    // * attribute: is_read_only_property
    public static readonly StyledProperty<bool> IsReadOnlyProperty =
        AvaloniaProperty.Register<TiferetTextBox, bool>(nameof(IsReadOnly));

    // * attribute: variant_property
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TiferetTextBox, string>(nameof(Variant), "outlined");

    // *** properties

    // ** property: text
    /// <summary>The current text value.</summary>
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // ** property: placeholder
    /// <summary>Placeholder text displayed when the input is empty.</summary>
    public string? Placeholder
    {
        get => GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    // ** property: is_read_only
    /// <summary>Whether the input is read-only.</summary>
    public bool IsReadOnly
    {
        get => GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    // ** property: variant
    /// <summary>Visual variant: "outlined" or "filled".</summary>
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
