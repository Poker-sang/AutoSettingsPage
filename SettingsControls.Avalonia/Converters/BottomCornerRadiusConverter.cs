using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace CommunityToolkit.Avalonia.Controls.Converters;

/// <summary>
/// Converts a <see cref="CornerRadius"/> to a new <see cref="CornerRadius"/> with only the bottom corners preserved.
/// </summary>
internal class BottomCornerRadiusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is CornerRadius cornerRadius)
        {
            return new CornerRadius(0, 0, cornerRadius.BottomRight, cornerRadius.BottomLeft);
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
