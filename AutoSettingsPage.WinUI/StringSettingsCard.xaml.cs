using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class StringSettingsCard : IEntryControl<ISingleValueSettingsEntry<string>>
{
    public ISingleValueSettingsEntry<string> Entry { get; set; } = null!;

    public StringSettingsCard() => InitializeComponent();
}
