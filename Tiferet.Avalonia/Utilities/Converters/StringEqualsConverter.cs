using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Tiferet.Avalonia.Utilities.Converters;

// *** converters

// ** converter: string_equals
/// <summary>
/// Returns <c>true</c> when the bound string equals the <c>ConverterParameter</c>
/// using case-insensitive ordinal comparison.
/// </summary>
public sealed class StringEqualsConverter : IValueConverter
{
    // * attribute: instance
    /// <summary>Singleton instance for XAML resource declarations.</summary>
    public static readonly StringEqualsConverter Instance = new();

    // * method: convert
    /// <summary>
    /// Compares the bound string value to the converter parameter.
    /// </summary>
    /// <param name="value">The bound value (expected string).</param>
    /// <param name="targetType">The target type (ignored).</param>
    /// <param name="parameter">The string to compare against.</param>
    /// <param name="culture">The culture (ignored).</param>
    /// <returns>
    /// <c>true</c> if both strings are equal (ordinal, case-insensitive);
    /// <c>false</c> otherwise or if either value is null.
    /// </returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Treat null on either side as not-equal.
        if (value is not string s || parameter is not string target)
            return false;

        // Perform case-insensitive ordinal comparison.
        return s.Equals(target, StringComparison.OrdinalIgnoreCase);
    }

    // * method: convert_back
    /// <summary>One-way converter — <see cref="ConvertBack"/> is not supported.</summary>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
