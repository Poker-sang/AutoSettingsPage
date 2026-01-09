using System.Collections.ObjectModel;
using AutoSettingsPage.Models;
using Microsoft.UI.Xaml.Media;

namespace AutoSettingsPage.WinUI;

public sealed partial class FontSettingsCard : IEntryControl<ISingleValueSettingsEntry<ObservableCollection<string>>>
{
    public ISingleValueSettingsEntry<ObservableCollection<string>> Entry { get; set; } = null!;

    public FontSettingsCard() => InitializeComponent();

    public static FontFamily ToFontFamily(string value) => new(value);
}
