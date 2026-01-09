using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class BoolSettingsCard : IEntryControl<ISingleValueSettingsEntry<bool>>
{
    public ISingleValueSettingsEntry<bool> Entry { get; set; } = null!;

    public BoolSettingsCard() => InitializeComponent();
}
