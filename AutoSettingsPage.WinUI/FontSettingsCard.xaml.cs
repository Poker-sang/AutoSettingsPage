using AutoSettingsPage.Models;
using Microsoft.UI.Xaml.Media;

namespace AutoSettingsPage.WinUI;

public sealed partial class FontSettingsCard : IEntryControl<ISingleValueSettingsEntry<string>>
{
    public ISingleValueSettingsEntry<string> Entry { get; set; } = null!;

    public FontSettingsCard() => InitializeComponent();

    public static FontFamily ToFontFamily(string value) => new(value);
}
