using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Tiferet.Avalonia.Assets.Design;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_text_block
/// <summary>
/// A typography-aware text block that resolves font properties
/// from the Tiferet type scale by name (e.g., "BodyMedium", "DisplayLarge").
/// </summary>
public class TiferetTextBlock : TemplatedControl
{
    // * attribute: type_scale_property
    public static readonly StyledProperty<string> TypeScaleProperty =
        AvaloniaProperty.Register<TiferetTextBlock, string>(nameof(TypeScale), "BodyMedium");

    // * attribute: text_property
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<TiferetTextBlock, string?>(nameof(Text));

    // *** properties

    /// <summary>
    /// The type scale name (e.g., "BodyMedium", "HeadlineLarge", "DisplaySmall").
    /// Resolves to font size, weight, and line height from <see cref="TiferetTypography"/>.
    /// </summary>
    public string TypeScale
    {
        get => GetValue(TypeScaleProperty);
        set => SetValue(TypeScaleProperty, value);
    }

    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // *** methods

    // ** method: on_property_changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == TypeScaleProperty)
        {
            ApplyTypeScale((string?)change.NewValue);
        }
    }

    // ** method: on_apply_template
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // Apply initial type scale.
        ApplyTypeScale(TypeScale);
    }

    // *** helpers

    private void ApplyTypeScale(string? scaleName)
    {
        if (scaleName is null) return;

        var style = TiferetTypography.Get(scaleName);
        if (style is null) return;

        FontSize = style.FontSize;
        FontWeight = style.FontWeight;
        FontFamily = new FontFamily(style.FontFamily);
    }
}
