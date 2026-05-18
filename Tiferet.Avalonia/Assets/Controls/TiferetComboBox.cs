using System.Collections;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_combo_box
/// <summary>
/// A themed dropdown selector with variant support.
/// Variants: "outlined" (default), "filled".
/// </summary>
public class TiferetComboBox : TemplatedControl
{
    // * attribute: items_property
    public static readonly StyledProperty<IEnumerable?> ItemsProperty =
        AvaloniaProperty.Register<TiferetComboBox, IEnumerable?>(nameof(Items));

    // * attribute: selected_item_property
    public static readonly StyledProperty<object?> SelectedItemProperty =
        AvaloniaProperty.Register<TiferetComboBox, object?>(nameof(SelectedItem));

    // * attribute: placeholder_property
    public static readonly StyledProperty<string?> PlaceholderProperty =
        AvaloniaProperty.Register<TiferetComboBox, string?>(nameof(Placeholder));

    // * attribute: variant_property
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TiferetComboBox, string>(nameof(Variant), "outlined");

    // *** properties

    // ** property: items
    /// <summary>The collection of items to display.</summary>
    public IEnumerable? Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    // ** property: selected_item
    /// <summary>The currently selected item.</summary>
    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    // ** property: placeholder
    /// <summary>Placeholder text when no item is selected.</summary>
    public string? Placeholder
    {
        get => GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
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
