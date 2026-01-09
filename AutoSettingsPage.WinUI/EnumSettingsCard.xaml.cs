using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class EnumSettingsCard : IEntryControl<IEnumSettingsEntry<object>>
{
    public IEnumSettingsEntry<object> Entry { get; set; } = null!;

    public EnumSettingsCard() => InitializeComponent();
}
