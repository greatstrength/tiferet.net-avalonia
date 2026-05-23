using System;
using System.Globalization;
using Tiferet.Avalonia.Utilities.Converters;

namespace Tiferet.Avalonia.Tests.Utilities.Converters;

// *** tests

// ** test: string_is_null_or_empty_converter
public class StringIsNullOrEmptyConverterTests
{
    private static object Convert(object? value, object? parameter = null)
        => StringIsNullOrEmptyConverter.Instance.Convert(
            value, typeof(bool), parameter, CultureInfo.InvariantCulture);

    // ** test: returns_true_for_null
    [Fact]
    public void Convert_ReturnsTrue_ForNull()
        => Assert.Equal(true, Convert(null));

    // ** test: returns_true_for_empty_string
    [Fact]
    public void Convert_ReturnsTrue_ForEmptyString()
        => Assert.Equal(true, Convert(""));

    // ** test: returns_true_for_whitespace
    [Fact]
    public void Convert_ReturnsTrue_ForWhitespace()
        => Assert.Equal(true, Convert("   "));

    // ** test: returns_false_for_content
    [Fact]
    public void Convert_ReturnsFalse_ForContent()
        => Assert.Equal(false, Convert("hello"));

    // ** test: negate_returns_false_for_null
    [Fact]
    public void Convert_Negate_ReturnsFalse_ForNull()
        => Assert.Equal(false, Convert(null, "negate"));

    // ** test: negate_returns_true_for_content
    [Fact]
    public void Convert_Negate_ReturnsTrue_ForContent()
        => Assert.Equal(true, Convert("hello", "negate"));

    // ** test: negate_is_case_insensitive
    [Fact]
    public void Convert_Negate_IsCaseInsensitive()
        => Assert.Equal(true, Convert("hello", "NEGATE"));

    // ** test: convert_back_throws
    [Fact]
    public void ConvertBack_Throws()
        => Assert.Throws<NotSupportedException>(
            () => StringIsNullOrEmptyConverter.Instance.ConvertBack(
                null, typeof(string), null, CultureInfo.InvariantCulture));
}

// ** test: string_equals_converter
public class StringEqualsConverterTests
{
    private static object Convert(object? value, object? parameter)
        => StringEqualsConverter.Instance.Convert(
            value, typeof(bool), parameter, CultureInfo.InvariantCulture);

    // ** test: returns_true_for_equal_strings
    [Fact]
    public void Convert_ReturnsTrue_ForEqualStrings()
        => Assert.Equal(true, Convert("hello", "hello"));

    // ** test: case_insensitive_match
    [Fact]
    public void Convert_ReturnsTrue_CaseInsensitive()
        => Assert.Equal(true, Convert("Hello", "HELLO"));

    // ** test: returns_false_for_different_strings
    [Fact]
    public void Convert_ReturnsFalse_ForDifferentStrings()
        => Assert.Equal(false, Convert("hello", "world"));

    // ** test: returns_false_when_value_is_null
    [Fact]
    public void Convert_ReturnsFalse_WhenValueIsNull()
        => Assert.Equal(false, Convert(null, "hello"));

    // ** test: returns_false_when_parameter_is_null
    [Fact]
    public void Convert_ReturnsFalse_WhenParameterIsNull()
        => Assert.Equal(false, Convert("hello", null));

    // ** test: convert_back_throws
    [Fact]
    public void ConvertBack_Throws()
        => Assert.Throws<NotSupportedException>(
            () => StringEqualsConverter.Instance.ConvertBack(
                null, typeof(string), null, CultureInfo.InvariantCulture));
}

// ** test: date_time_format_converter
public class DateTimeFormatConverterTests
{
    private static object Convert(object? value, object? parameter = null)
        => DateTimeFormatConverter.Instance.Convert(
            value, typeof(string), parameter, CultureInfo.InvariantCulture);

    // ** test: formats_datetimeoffset
    [Fact]
    public void Convert_FormatsDateTimeOffset()
    {
        var dto = new DateTimeOffset(2025, 6, 15, 14, 30, 0, TimeSpan.Zero);
        Assert.Equal("2025-06-15", Convert(dto, "yyyy-MM-dd"));
    }

    // ** test: formats_datetime
    [Fact]
    public void Convert_FormatsDateTime()
    {
        var dt = new DateTime(2025, 6, 15, 14, 30, 0, DateTimeKind.Utc);
        Assert.Equal("14:30", Convert(dt, "HH:mm"));
    }

    // ** test: formats_iso_string
    [Fact]
    public void Convert_FormatsIsoString()
    {
        Assert.Equal("2025-06-15", Convert("2025-06-15T14:30:00Z", "yyyy-MM-dd"));
    }

    // ** test: returns_empty_for_null
    [Fact]
    public void Convert_ReturnsEmpty_ForNull()
        => Assert.Equal(string.Empty, Convert(null));

    // ** test: returns_empty_for_invalid_iso_string
    [Fact]
    public void Convert_ReturnsEmpty_ForInvalidIsoString()
        => Assert.Equal(string.Empty, Convert("not-a-date", "yyyy-MM-dd"));

    // ** test: returns_empty_for_unsupported_type
    [Fact]
    public void Convert_ReturnsEmpty_ForUnsupportedType()
        => Assert.Equal(string.Empty, Convert(12345));

    // ** test: convert_back_throws
    [Fact]
    public void ConvertBack_Throws()
        => Assert.Throws<NotSupportedException>(
            () => DateTimeFormatConverter.Instance.ConvertBack(
                null, typeof(string), null, CultureInfo.InvariantCulture));
}

// ** test: bool_to_visibility_converter
public class BoolToVisibilityConverterTests
{
    private static object Convert(object? value, object? parameter = null)
        => BoolToVisibilityConverter.Instance.Convert(
            value, typeof(bool), parameter, CultureInfo.InvariantCulture);

    // ** test: returns_true_for_true
    [Fact]
    public void Convert_ReturnsTrue_ForTrue()
        => Assert.Equal(true, Convert(true));

    // ** test: returns_false_for_false
    [Fact]
    public void Convert_ReturnsFalse_ForFalse()
        => Assert.Equal(false, Convert(false));

    // ** test: returns_false_for_null
    [Fact]
    public void Convert_ReturnsFalse_ForNull()
        => Assert.Equal(false, Convert(null));

    // ** test: negate_returns_false_for_true
    [Fact]
    public void Convert_Negate_ReturnsFalse_ForTrue()
        => Assert.Equal(false, Convert(true, "negate"));

    // ** test: negate_returns_true_for_false
    [Fact]
    public void Convert_Negate_ReturnsTrue_ForFalse()
        => Assert.Equal(true, Convert(false, "negate"));

    // ** test: negate_is_case_insensitive
    [Fact]
    public void Convert_Negate_IsCaseInsensitive()
        => Assert.Equal(false, Convert(true, "NEGATE"));

    // ** test: convert_back_throws
    [Fact]
    public void ConvertBack_Throws()
        => Assert.Throws<NotSupportedException>(
            () => BoolToVisibilityConverter.Instance.ConvertBack(
                null, typeof(string), null, CultureInfo.InvariantCulture));
}
