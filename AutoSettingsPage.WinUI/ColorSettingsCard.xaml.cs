using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AutoSettingsPage.Models;
using Windows.UI;

namespace AutoSettingsPage.WinUI;

public sealed partial class ColorSettingsCard : IEntryControl<ISingleValueSettingsEntry<uint>>
{
    public ISingleValueSettingsEntry<uint> Entry { get; set; } = null!;

    public ColorSettingsCard() => InitializeComponent();

    public static Color ToAlphaColor(uint color)
    {
        var span = MemoryMarshal.CreateSpan(ref Unsafe.As<uint, byte>(ref color), 4);
        return Color.FromArgb(span[3], span[2], span[1], span[0]);
    }

    private void ColorBindBack(Color color)
    {
        uint ret = 0;
        var span = MemoryMarshal.CreateSpan(ref Unsafe.As<uint, byte>(ref ret), 4);
        span[0] = color.B;
        span[1] = color.G;
        span[2] = color.R;
        span[3] = color.A;
        Entry.Value = ret;
    }
}
