using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class StringSettingsCard : SettingsCard, IEntryControl<ISingleValueSettingsEntry<string>>
{
    public ISingleValueSettingsEntry<string> Entry { set => DataContext = value; }

    public StringSettingsCard() => InitializeComponent();
}
