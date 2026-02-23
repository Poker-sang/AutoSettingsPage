using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AutoSettingsPage.Models;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class ColorSettingsCard : SettingsCard, IEntryControl<ISingleValueSettingsEntry<uint>>
{
    public static FuncValueConverter<uint, Color> ColorConverter { get; } = new(ToColor, FromColor);

    public ISingleValueSettingsEntry<uint> Entry { set => DataContext = value; }

    public ColorSettingsCard() => InitializeComponent();

    public static Color ToColor(uint color)
    {
        var span = MemoryMarshal.CreateSpan(ref Unsafe.As<uint, byte>(ref color), 4);
        return Color.FromArgb(span[3], span[2], span[1], span[0]);
    }

    public static uint FromColor(Color color)
    {
        uint ret = 0;
        var span = MemoryMarshal.CreateSpan(ref Unsafe.As<uint, byte>(ref ret), 4);
        span[0] = color.B;
        span[1] = color.G;
        span[2] = color.R;
        span[3] = color.A;
        return ret;
    }
}
