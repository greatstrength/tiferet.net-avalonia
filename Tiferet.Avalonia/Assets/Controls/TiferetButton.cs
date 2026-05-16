using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_button
/// <summary>
/// A styled button with variant and size support.
/// Variants: "filled" (default), "outlined", "text", "tonal".
/// Sizes: "sm", "md" (default), "lg".
/// </summary>
public class TiferetButton : TemplatedControl
{
    // * attribute: variant_property
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TiferetButton, string>(nameof(Variant), "filled");

    // * attribute: size_property
    public static readonly StyledProperty<string> SizeProperty =
        AvaloniaProperty.Register<TiferetButton, string>(nameof(Size), "md");

    // * attribute: content_property
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<TiferetButton, object?>(nameof(Content));

    // * attribute: command_property
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<TiferetButton, ICommand?>(nameof(Command));

    // * attribute: command_parameter_property
    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<TiferetButton, object?>(nameof(CommandParameter));

    // *** properties

    // ** property: variant
    /// <summary>Visual variant: "filled", "outlined", "text", or "tonal".</summary>
    public string Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    // ** property: size
    /// <summary>Size: "sm", "md", or "lg".</summary>
    public string Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // ** property: content
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    // ** property: command
    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    // ** property: command_parameter
    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    // *** methods

    // ** method: on_property_changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        // Update pseudo-classes when Variant or Size changes.
        if (change.Property == VariantProperty)
        {
            UpdateVariantPseudoClasses((string?)change.OldValue, (string?)change.NewValue);
        }
        else if (change.Property == SizeProperty)
        {
            UpdateSizePseudoClasses((string?)change.OldValue, (string?)change.NewValue);
        }
    }

    // ** method: on_apply_template
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // Set initial pseudo-classes.
        UpdateVariantPseudoClasses(null, Variant);
        UpdateSizePseudoClasses(null, Size);
    }

    // ** method: on_pointer_pressed
    protected override void OnPointerPressed(global::Avalonia.Input.PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        // Execute command on press.
        if (Command?.CanExecute(CommandParameter) == true)
            Command.Execute(CommandParameter);
    }

    // *** helpers

    private void UpdateVariantPseudoClasses(string? oldValue, string? newValue)
    {
        if (oldValue is not null) PseudoClasses.Remove($":{oldValue}");
        if (newValue is not null) PseudoClasses.Add($":{newValue}");
    }

    private void UpdateSizePseudoClasses(string? oldValue, string? newValue)
    {
        if (oldValue is not null) PseudoClasses.Remove($":{oldValue}");
        if (newValue is not null) PseudoClasses.Add($":{newValue}");
    }
}
