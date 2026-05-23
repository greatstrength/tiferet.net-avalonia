using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Tiferet.Avalonia.Utilities.Converters;

// *** converters

// ** converter: string_is_null_or_empty
/// <summary>
/// Returns <c>true</c> when the bound string is null, empty, or whitespace.
/// Pass <c>"negate"</c> as <see cref="IValueConverter.Convert"/> parameter to invert
/// the result (i.e. <c>true</c> when the string has content).
/// </summary>
public sealed class StringIsNullOrEmptyConverter : IValueConverter
{
    // * attribute: instance
    /// <summary>Singleton instance for XAML resource declarations.</summary>
    public static readonly StringIsNullOrEmptyConverter Instance = new();

    // * method: convert
    /// <summary>
    /// Converts a string value to a boolean indicating whether it is null or empty.
    /// </summary>
    /// <param name="value">The bound value (expected string).</param>
    /// <param name="targetType">The target type (ignored).</param>
    /// <param name="parameter">
    /// Optional. Pass <c>"negate"</c> (case-insensitive) to invert the result.
    /// </param>
    /// <param name="culture">The culture (ignored).</param>
    /// <returns>
    /// <c>true</c> if the string is null/empty/whitespace (or <c>false</c> when negated);
    /// otherwise <c>false</c> (or <c>true</c> when negated).
    /// </returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Determine whether the string is null, empty, or whitespace.
        var isEmpty = string.IsNullOrWhiteSpace(value as string);

        // Check whether the caller wants the negated result.
        var negate = parameter is string p
            && p.Equals("negate", StringComparison.OrdinalIgnoreCase);

        // Return the (possibly negated) result.
        return negate ? !isEmpty : isEmpty;
    }

    // * method: convert_back
    /// <summary>One-way converter — <see cref="ConvertBack"/> is not supported.</summary>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
