using Avalonia;
using Avalonia.Controls.Primitives;

namespace Tiferet.Avalonia.Assets.Controls;

// *** controls

// ** control: tiferet_card
/// <summary>
/// A surface container with elevation support.
/// Provides optional Header, Content, and Footer slots.
/// </summary>
public class TiferetCard : TemplatedControl
{
    // * attribute: elevation_property
    public static readonly StyledProperty<int> ElevationProperty =
        AvaloniaProperty.Register<TiferetCard, int>(nameof(Elevation), 1);

    // * attribute: header_property
    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<TiferetCard, object?>(nameof(Header));

    // * attribute: content_property
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<TiferetCard, object?>(nameof(Content));

    // * attribute: footer_property
    public static readonly StyledProperty<object?> FooterProperty =
        AvaloniaProperty.Register<TiferetCard, object?>(nameof(Footer));

    // *** properties

    /// <summary>Elevation level (0–4). Controls shadow depth.</summary>
    public int Elevation
    {
        get => GetValue(ElevationProperty);
        set => SetValue(ElevationProperty, value);
    }

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }
}
