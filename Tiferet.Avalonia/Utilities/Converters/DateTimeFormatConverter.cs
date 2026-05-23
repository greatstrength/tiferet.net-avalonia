using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Tiferet.Avalonia.Utilities.Converters;

// *** converters

// ** converter: date_time_format
/// <summary>
/// Converts a <see cref="DateTimeOffset"/>, <see cref="DateTime"/>, or ISO 8601 string
/// to a formatted string using the format pattern supplied via <c>ConverterParameter</c>
/// (e.g. <c>"HH:mm"</c>, <c>"yyyy-MM-dd"</c>).
/// Returns an empty string on parse failure or null input.
/// </summary>
public sealed class DateTimeFormatConverter : IValueConverter
{
    // * attribute: instance
    /// <summary>Singleton instance for XAML resource declarations.</summary>
    public static readonly DateTimeFormatConverter Instance = new();

    // * method: convert
    /// <summary>
    /// Formats the bound date/time value using the parameter as a format string.
    /// </summary>
    /// <param name="value">
    /// The bound value — <see cref="DateTimeOffset"/>, <see cref="DateTime"/>,
    /// or an ISO 8601 string.
    /// </param>
    /// <param name="targetType">The target type (ignored).</param>
    /// <param name="parameter">The format string (e.g. <c>"yyyy-MM-dd"</c>).</param>
    /// <param name="culture">The culture used for formatting.</param>
    /// <returns>The formatted string, or <see cref="string.Empty"/> on failure.</returns>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Determine the format pattern; fall back to general sortable if none supplied.
        var format = parameter as string ?? "s";

        // Resolve the DateTimeOffset from the bound value.
        DateTimeOffset dto;

        if (value is DateTimeOffset offset)
        {
            dto = offset;
        }
        else if (value is DateTime dt)
        {
            dto = new DateTimeOffset(dt);
        }
        else if (value is string iso)
        {
            // Attempt to parse an ISO 8601 string.
            if (!DateTimeOffset.TryParse(iso, CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind, out dto))
                return string.Empty;
        }
        else
        {
            // Null or unsupported type.
            return string.Empty;
        }

        // Format and return.
        try
        {
            return dto.ToString(format, culture);
        }
        catch (FormatException)
        {
            return string.Empty;
        }
    }

    // * method: convert_back
    /// <summary>One-way converter — <see cref="ConvertBack"/> is not supported.</summary>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
