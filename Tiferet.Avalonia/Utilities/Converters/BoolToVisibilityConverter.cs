using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Tiferet.Avalonia.Utilities.Converters;

// *** converters

// ** converter: bool_to_visibility
/// <summary>
/// Converts a <see cref="bool"/> to a visibility-compatible boolean for use with
/// Avalonia's <c>IsVisible</c> property. Pass <c>"negate"</c> as the
/// <c>ConverterParameter</c> to invert the logic.
/// </summary>
public sealed class BoolToVisibilityConverter : IValueConverter
{
    // * attribute: instance
    /// <summary>Singleton instance for XAML resource declarations.</summary>
    public static readonly BoolToVisibilityConverter Instance = new();

    // * method: convert
    /// <summary>
    /// Converts a boolean value to an <c>IsVisible</c>-compatible boolean.
    /// </summary>
    /// <param name="value">The bound boolean value.</param>
    /// <param name="targetType">The target type (ignored).</param>
    /// <param name="parameter">
    /// Optional. Pass <c>"negate"</c> (case-insensitive) to invert the result.
    /// </param>
    /// <param name="culture">The culture (ignored).</param>
    /// <returns>
    /// The boolean value (or its inverse when <c>"negate"</c> is supplied).
    /// Returns <c>false</c> for null input.
    /// </returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Treat null as false.
        var visible = value is true;

        // Check whether the caller wants the negated result.
        var negate = parameter is string p
            && p.Equals("negate", StringComparison.OrdinalIgnoreCase);

        // Return the (possibly negated) visibility boolean.
        return negate ? !visible : visible;
    }

    // * method: convert_back
    /// <summary>One-way converter — <see cref="ConvertBack"/> is not supported.</summary>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
