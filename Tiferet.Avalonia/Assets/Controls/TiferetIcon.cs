using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Tiferet.Avalonia.Assets.Design;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_icon
/// <summary>
/// Renders an icon from the <see cref="TiferetIcons"/> registry as an SVG path.
/// Set <see cref="Kind"/> to a registered icon name (e.g., "home", "search").
/// </summary>
public class TiferetIcon : TemplatedControl
{
    // * attribute: kind_property
    public static readonly StyledProperty<string?> KindProperty =
        AvaloniaProperty.Register<TiferetIcon, string?>(nameof(Kind));

    // * attribute: size_property
    public static readonly StyledProperty<double> SizeProperty =
        AvaloniaProperty.Register<TiferetIcon, double>(nameof(Size), 24);

    // * attribute: color_property
    public static readonly StyledProperty<IBrush?> ColorProperty =
        AvaloniaProperty.Register<TiferetIcon, IBrush?>(nameof(Color));

    // * attribute: geometry_property
    public static readonly StyledProperty<Geometry?> GeometryProperty =
        AvaloniaProperty.Register<TiferetIcon, Geometry?>(nameof(Geometry));

    // *** properties

    // ** property: kind
    /// <summary>The icon name from the <see cref="TiferetIcons"/> registry.</summary>
    public string? Kind
    {
        get => GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    // ** property: size
    /// <summary>The icon size in pixels (width and height).</summary>
    public double Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // ** property: color
    /// <summary>The icon fill color. Defaults to OnSurface theme brush.</summary>
    public IBrush? Color
    {
        get => GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    // ** property: geometry
    /// <summary>The resolved path geometry (set automatically from <see cref="Kind"/>).</summary>
    public Geometry? Geometry
    {
        get => GetValue(GeometryProperty);
        set => SetValue(GeometryProperty, value);
    }

    // *** methods

    // ** method: on_property_changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == KindProperty)
        {
            ResolveGeometry((string?)change.NewValue);
        }
    }

    // ** method: on_apply_template
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        ResolveGeometry(Kind);
    }

    // *** helpers

    private void ResolveGeometry(string? kind)
    {
        if (kind is null)
        {
            Geometry = null;
            return;
        }

        var pathData = TiferetIcons.Get(kind);
        if (pathData is null)
        {
            Geometry = null;
            return;
        }

        try
        {
            Geometry = Geometry.Parse(pathData);
        }
        catch (InvalidOperationException)
        {
            // Avalonia render platform not available (e.g., headless tests).
            Geometry = null;
        }
    }
}
