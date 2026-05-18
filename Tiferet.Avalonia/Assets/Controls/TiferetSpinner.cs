using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_spinner
/// <summary>
/// A rotating loading indicator.
/// Sizes: "sm" (16px), "md" (32px, default), "lg" (48px).
/// </summary>
public class TiferetSpinner : TemplatedControl
{
    // * attribute: size_property
    public static readonly StyledProperty<string> SizeProperty =
        AvaloniaProperty.Register<TiferetSpinner, string>(nameof(Size), "md");

    // * attribute: is_active_property
    public static readonly StyledProperty<bool> IsActiveProperty =
        AvaloniaProperty.Register<TiferetSpinner, bool>(nameof(IsActive), true);

    // *** properties

    // ** property: size
    /// <summary>Size: "sm", "md", or "lg".</summary>
    public string Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // ** property: is_active
    /// <summary>Whether the spinner is actively animating.</summary>
    public bool IsActive
    {
        get => GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    // *** methods

    // ** method: on_property_changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SizeProperty)
        {
            if (change.OldValue is string old) PseudoClasses.Remove($":{old}");
            if (change.NewValue is string nv) PseudoClasses.Add($":{nv}");
        }
        else if (change.Property == IsActiveProperty)
        {
            var isActive = (bool)change.NewValue!;
            if (isActive) PseudoClasses.Add(":active");
            else PseudoClasses.Remove(":active");
        }
    }

    // ** method: on_apply_template
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        PseudoClasses.Add($":{Size}");
        if (IsActive) PseudoClasses.Add(":active");
    }
}
