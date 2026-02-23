using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class BoolSettingsCard : SettingsCard, IEntryControl<ISingleValueSettingsEntry<bool>>
{
    public ISingleValueSettingsEntry<bool> Entry { set => DataContext = value; }

    public BoolSettingsCard() => InitializeComponent();
}
