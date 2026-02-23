using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AutoSettingsPage.Avalonia;

public sealed class DoubleDecimalConverter : IValueConverter
{
    public static readonly DoubleDecimalConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is double d ? (decimal)d : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is decimal m ? (double)m : 0d;
}
