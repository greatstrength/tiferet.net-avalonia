using Avalonia.Media;

namespace Tiferet.Avalonia.Assets.Design;

// *** records

// ** record: type_style
/// <summary>
/// Defines a single entry in the typographic scale.
/// </summary>
public sealed record TypeStyle(
    string FontFamily,
    double FontSize,
    FontWeight FontWeight,
    double LineHeight,
    double LetterSpacing);

// *** constants

// ** constant: tiferet_typography
/// <summary>
/// The Tiferet type scale. Follows a Material Design-inspired hierarchy
/// with display, headline, title, body, and label tiers.
/// Default font family is Inter (loaded via Avalonia's WithInterFont()).
/// </summary>
public static class TiferetTypography
{
    // * constant: default_font_family
    public const string DefaultFontFamily = "Inter, Segoe UI, Helvetica, Arial, sans-serif";

    // * constant: display_large
    public static readonly TypeStyle DisplayLarge = new(
        DefaultFontFamily, FontSize: 57, FontWeight: FontWeight.Regular, LineHeight: 64, LetterSpacing: -0.25);

    // * constant: display_medium
    public static readonly TypeStyle DisplayMedium = new(
        DefaultFontFamily, FontSize: 45, FontWeight: FontWeight.Regular, LineHeight: 52, LetterSpacing: 0);

    // * constant: display_small
    public static readonly TypeStyle DisplaySmall = new(
        DefaultFontFamily, FontSize: 36, FontWeight: FontWeight.Regular, LineHeight: 44, LetterSpacing: 0);

    // * constant: headline_large
    public static readonly TypeStyle HeadlineLarge = new(
        DefaultFontFamily, FontSize: 32, FontWeight: FontWeight.Regular, LineHeight: 40, LetterSpacing: 0);

    // * constant: headline_medium
    public static readonly TypeStyle HeadlineMedium = new(
        DefaultFontFamily, FontSize: 28, FontWeight: FontWeight.Regular, LineHeight: 36, LetterSpacing: 0);

    // * constant: headline_small
    public static readonly TypeStyle HeadlineSmall = new(
        DefaultFontFamily, FontSize: 24, FontWeight: FontWeight.Regular, LineHeight: 32, LetterSpacing: 0);

    // * constant: title_large
    public static readonly TypeStyle TitleLarge = new(
        DefaultFontFamily, FontSize: 22, FontWeight: FontWeight.Medium, LineHeight: 28, LetterSpacing: 0);

    // * constant: title_medium
    public static readonly TypeStyle TitleMedium = new(
        DefaultFontFamily, FontSize: 16, FontWeight: FontWeight.Medium, LineHeight: 24, LetterSpacing: 0.15);

    // * constant: title_small
    public static readonly TypeStyle TitleSmall = new(
        DefaultFontFamily, FontSize: 14, FontWeight: FontWeight.Medium, LineHeight: 20, LetterSpacing: 0.1);

    // * constant: body_large
    public static readonly TypeStyle BodyLarge = new(
        DefaultFontFamily, FontSize: 16, FontWeight: FontWeight.Regular, LineHeight: 24, LetterSpacing: 0.5);

    // * constant: body_medium
    public static readonly TypeStyle BodyMedium = new(
        DefaultFontFamily, FontSize: 14, FontWeight: FontWeight.Regular, LineHeight: 20, LetterSpacing: 0.25);

    // * constant: body_small
    public static readonly TypeStyle BodySmall = new(
        DefaultFontFamily, FontSize: 12, FontWeight: FontWeight.Regular, LineHeight: 16, LetterSpacing: 0.4);

    // * constant: label_large
    public static readonly TypeStyle LabelLarge = new(
        DefaultFontFamily, FontSize: 14, FontWeight: FontWeight.Medium, LineHeight: 20, LetterSpacing: 0.1);

    // * constant: label_medium
    public static readonly TypeStyle LabelMedium = new(
        DefaultFontFamily, FontSize: 12, FontWeight: FontWeight.Medium, LineHeight: 16, LetterSpacing: 0.5);

    // * constant: label_small
    public static readonly TypeStyle LabelSmall = new(
        DefaultFontFamily, FontSize: 11, FontWeight: FontWeight.Medium, LineHeight: 16, LetterSpacing: 0.5);

    // *** methods

    // ** method: get
    /// <summary>
    /// Resolve a <see cref="TypeStyle"/> by its scale name (e.g., "BodyMedium", "DisplayLarge").
    /// Returns null if the name is not recognized.
    /// </summary>
    public static TypeStyle? Get(string scaleName) => scaleName switch
    {
        nameof(DisplayLarge)   => DisplayLarge,
        nameof(DisplayMedium)  => DisplayMedium,
        nameof(DisplaySmall)   => DisplaySmall,
        nameof(HeadlineLarge)  => HeadlineLarge,
        nameof(HeadlineMedium) => HeadlineMedium,
        nameof(HeadlineSmall)  => HeadlineSmall,
        nameof(TitleLarge)     => TitleLarge,
        nameof(TitleMedium)    => TitleMedium,
        nameof(TitleSmall)     => TitleSmall,
        nameof(BodyLarge)      => BodyLarge,
        nameof(BodyMedium)     => BodyMedium,
        nameof(BodySmall)      => BodySmall,
        nameof(LabelLarge)     => LabelLarge,
        nameof(LabelMedium)    => LabelMedium,
        nameof(LabelSmall)     => LabelSmall,
        _                      => null,
    };
}
