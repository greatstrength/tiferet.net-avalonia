using Avalonia;

namespace Tiferet.Avalonia.Assets.Design;

// *** constants

// ** constant: spacing
/// <summary>
/// Spacing scale for margins, padding, and gaps.
/// </summary>
public static class Spacing
{
    public const double None = 0;
    public const double XS   = 4;
    public const double SM   = 8;
    public const double MD   = 16;
    public const double LG   = 24;
    public const double XL   = 32;
    public const double XXL  = 48;
}

// ** constant: corner_radii
/// <summary>
/// Corner radius tokens for consistent shape language.
/// </summary>
public static class CornerRadii
{
    public static readonly CornerRadius None = new(0);
    public static readonly CornerRadius SM   = new(4);
    public static readonly CornerRadius MD   = new(8);
    public static readonly CornerRadius LG   = new(12);
    public static readonly CornerRadius XL   = new(16);
    public static readonly CornerRadius Full = new(999);
}

// ** constant: elevation
/// <summary>
/// Elevation shadow definitions for surface depth (levels 0–4).
/// Expressed as Avalonia <see cref="BoxShadows"/> strings.
/// </summary>
public static class Elevation
{
    public const string Level0 = "0 0 0 0 transparent";
    public const string Level1 = "0 1 3 0 #1A000000, 0 1 2 -1 #1A000000";
    public const string Level2 = "0 3 6 0 #26000000, 0 2 4 -2 #1A000000";
    public const string Level3 = "0 6 10 0 #26000000, 0 3 6 -3 #1A000000";
    public const string Level4 = "0 8 16 0 #33000000, 0 4 8 -4 #1A000000";

    /// <summary>
    /// Resolve an elevation level (0–4) to its shadow string.
    /// </summary>
    public static string ForLevel(int level) => level switch
    {
        0 => Level0,
        1 => Level1,
        2 => Level2,
        3 => Level3,
        4 => Level4,
        _ => level < 0 ? Level0 : Level4,
    };
}

// ** constant: duration
/// <summary>
/// Animation duration tokens.
/// </summary>
public static class Duration
{
    public static readonly TimeSpan Fast   = TimeSpan.FromMilliseconds(100);
    public static readonly TimeSpan Normal = TimeSpan.FromMilliseconds(200);
    public static readonly TimeSpan Slow   = TimeSpan.FromMilliseconds(300);
}

// ** constant: opacity
/// <summary>
/// Opacity tokens for disabled, medium, and high emphasis states.
/// </summary>
public static class Opacity
{
    public const double Disabled = 0.38;
    public const double Medium   = 0.60;
    public const double High     = 0.87;
}
