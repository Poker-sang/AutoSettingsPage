using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class EnumSettingsCard : SettingsCard, IEntryControl<IEnumSettingsEntry<object>>
{
    public IEnumSettingsEntry<object> Entry { set => DataContext = value; }

    public EnumSettingsCard() => InitializeComponent();
}

