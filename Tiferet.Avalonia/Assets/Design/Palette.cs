using Avalonia.Media;

namespace Tiferet.Avalonia.Assets.Design;

// *** records

// ** record: color_scale
/// <summary>
/// A graduated color scale from lightest (50) to darkest (900).
/// </summary>
public sealed record ColorScale(
    Color S50,
    Color S100,
    Color S200,
    Color S300,
    Color S400,
    Color S500,
    Color S600,
    Color S700,
    Color S800,
    Color S900);

// ** record: semantic_colors
/// <summary>
/// Semantic colors for status and feedback.
/// </summary>
public sealed record SemanticColors(
    Color Error,
    Color ErrorContainer,
    Color Success,
    Color SuccessContainer,
    Color Warning,
    Color WarningContainer,
    Color Info,
    Color InfoContainer);

// ** record: tiferet_palette
/// <summary>
/// Immutable palette definition for the Tiferet design system.
/// The default palette draws from a blue-indigo primary inspired
/// by the Kabbalistic concept of Tiferet (beauty and harmony).
/// </summary>
public sealed record TiferetPalette(
    ColorScale Primary,
    ColorScale Secondary,
    ColorScale Neutral,
    SemanticColors Semantic,
    Color Background,
    Color Surface,
    Color OnPrimary,
    Color OnSecondary,
    Color OnBackground,
    Color OnSurface,
    Color Outline)
{
    // *** factories

    // ** factory: light
    /// <summary>
    /// Creates the default light theme palette.
    /// Primary: blue-indigo. Secondary: teal. Neutral: slate gray.
    /// </summary>
    public static TiferetPalette Light() => new(
        Primary: new ColorScale(
            S50:  Color.Parse("#EEF2FF"),
            S100: Color.Parse("#E0E7FF"),
            S200: Color.Parse("#C7D2FE"),
            S300: Color.Parse("#A5B4FC"),
            S400: Color.Parse("#818CF8"),
            S500: Color.Parse("#6366F1"),
            S600: Color.Parse("#4F46E5"),
            S700: Color.Parse("#4338CA"),
            S800: Color.Parse("#3730A3"),
            S900: Color.Parse("#312E81")),
        Secondary: new ColorScale(
            S50:  Color.Parse("#F0FDFA"),
            S100: Color.Parse("#CCFBF1"),
            S200: Color.Parse("#99F6E4"),
            S300: Color.Parse("#5EEAD4"),
            S400: Color.Parse("#2DD4BF"),
            S500: Color.Parse("#14B8A6"),
            S600: Color.Parse("#0D9488"),
            S700: Color.Parse("#0F766E"),
            S800: Color.Parse("#115E59"),
            S900: Color.Parse("#134E4A")),
        Neutral: new ColorScale(
            S50:  Color.Parse("#F8FAFC"),
            S100: Color.Parse("#F1F5F9"),
            S200: Color.Parse("#E2E8F0"),
            S300: Color.Parse("#CBD5E1"),
            S400: Color.Parse("#94A3B8"),
            S500: Color.Parse("#64748B"),
            S600: Color.Parse("#475569"),
            S700: Color.Parse("#334155"),
            S800: Color.Parse("#1E293B"),
            S900: Color.Parse("#0F172A")),
        Semantic: new SemanticColors(
            Error:            Color.Parse("#DC2626"),
            ErrorContainer:   Color.Parse("#FEE2E2"),
            Success:          Color.Parse("#16A34A"),
            SuccessContainer: Color.Parse("#DCFCE7"),
            Warning:          Color.Parse("#D97706"),
            WarningContainer: Color.Parse("#FEF3C7"),
            Info:             Color.Parse("#2563EB"),
            InfoContainer:    Color.Parse("#DBEAFE")),
        Background:  Color.Parse("#FFFFFF"),
        Surface:     Color.Parse("#F8FAFC"),
        OnPrimary:   Color.Parse("#FFFFFF"),
        OnSecondary: Color.Parse("#FFFFFF"),
        OnBackground: Color.Parse("#0F172A"),
        OnSurface:   Color.Parse("#1E293B"),
        Outline:     Color.Parse("#CBD5E1"));

    // ** factory: custom
    /// <summary>
    /// Creates a custom palette by selectively overriding colors from a base palette.
    /// Unspecified values are inherited from the base (defaults to <see cref="Light"/>).
    /// </summary>
    /// <param name="basePalette">The base palette to inherit defaults from. Defaults to <see cref="Light"/>.</param>
    /// <param name="primary">Optional primary color scale override.</param>
    /// <param name="secondary">Optional secondary color scale override.</param>
    /// <param name="neutral">Optional neutral color scale override.</param>
    /// <param name="semantic">Optional semantic colors override.</param>
    /// <param name="background">Optional background color override.</param>
    /// <param name="surface">Optional surface color override.</param>
    /// <param name="onPrimary">Optional on-primary color override.</param>
    /// <param name="onSecondary">Optional on-secondary color override.</param>
    /// <param name="onBackground">Optional on-background color override.</param>
    /// <param name="onSurface">Optional on-surface color override.</param>
    /// <param name="outline">Optional outline color override.</param>
    public static TiferetPalette Custom(
        TiferetPalette? basePalette = null,
        ColorScale? primary = null,
        ColorScale? secondary = null,
        ColorScale? neutral = null,
        SemanticColors? semantic = null,
        Color? background = null,
        Color? surface = null,
        Color? onPrimary = null,
        Color? onSecondary = null,
        Color? onBackground = null,
        Color? onSurface = null,
        Color? outline = null)
    {
        var b = basePalette ?? Light();
        return new TiferetPalette(
            Primary:      primary ?? b.Primary,
            Secondary:    secondary ?? b.Secondary,
            Neutral:      neutral ?? b.Neutral,
            Semantic:     semantic ?? b.Semantic,
            Background:   background ?? b.Background,
            Surface:      surface ?? b.Surface,
            OnPrimary:    onPrimary ?? b.OnPrimary,
            OnSecondary:  onSecondary ?? b.OnSecondary,
            OnBackground: onBackground ?? b.OnBackground,
            OnSurface:    onSurface ?? b.OnSurface,
            Outline:      outline ?? b.Outline);
    }

    // ** factory: dark
    /// <summary>
    /// Creates the default dark theme palette.
    /// Inverted luminance with the same hue families.
    /// </summary>
    public static TiferetPalette Dark() => new(
        Primary: new ColorScale(
            S50:  Color.Parse("#312E81"),
            S100: Color.Parse("#3730A3"),
            S200: Color.Parse("#4338CA"),
            S300: Color.Parse("#4F46E5"),
            S400: Color.Parse("#6366F1"),
            S500: Color.Parse("#818CF8"),
            S600: Color.Parse("#A5B4FC"),
            S700: Color.Parse("#C7D2FE"),
            S800: Color.Parse("#E0E7FF"),
            S900: Color.Parse("#EEF2FF")),
        Secondary: new ColorScale(
            S50:  Color.Parse("#134E4A"),
            S100: Color.Parse("#115E59"),
            S200: Color.Parse("#0F766E"),
            S300: Color.Parse("#0D9488"),
            S400: Color.Parse("#14B8A6"),
            S500: Color.Parse("#2DD4BF"),
            S600: Color.Parse("#5EEAD4"),
            S700: Color.Parse("#99F6E4"),
            S800: Color.Parse("#CCFBF1"),
            S900: Color.Parse("#F0FDFA")),
        Neutral: new ColorScale(
            S50:  Color.Parse("#0F172A"),
            S100: Color.Parse("#1E293B"),
            S200: Color.Parse("#334155"),
            S300: Color.Parse("#475569"),
            S400: Color.Parse("#64748B"),
            S500: Color.Parse("#94A3B8"),
            S600: Color.Parse("#CBD5E1"),
            S700: Color.Parse("#E2E8F0"),
            S800: Color.Parse("#F1F5F9"),
            S900: Color.Parse("#F8FAFC")),
        Semantic: new SemanticColors(
            Error:            Color.Parse("#F87171"),
            ErrorContainer:   Color.Parse("#7F1D1D"),
            Success:          Color.Parse("#4ADE80"),
            SuccessContainer: Color.Parse("#14532D"),
            Warning:          Color.Parse("#FBBF24"),
            WarningContainer: Color.Parse("#78350F"),
            Info:             Color.Parse("#60A5FA"),
            InfoContainer:    Color.Parse("#1E3A5F")),
        Background:  Color.Parse("#0F172A"),
        Surface:     Color.Parse("#1E293B"),
        OnPrimary:   Color.Parse("#0F172A"),
        OnSecondary: Color.Parse("#0F172A"),
        OnBackground: Color.Parse("#F1F5F9"),
        OnSurface:   Color.Parse("#E2E8F0"),
        Outline:     Color.Parse("#475569"));
}
