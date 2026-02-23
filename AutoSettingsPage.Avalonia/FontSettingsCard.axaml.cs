using AutoSettingsPage.Models;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class FontSettingsCard : SettingsCard, IEntryControl<ISingleValueSettingsEntry<string>>
{
    public static FuncValueConverter<string, FontFamily> FontFamilyConverter { get; } = new(v => new FontFamily(v ?? string.Empty));

    public ISingleValueSettingsEntry<string> Entry { set => DataContext = value; }

    public FontSettingsCard() => InitializeComponent();

    public static FontFamily ToFontFamily(string value) => new(value);
}
