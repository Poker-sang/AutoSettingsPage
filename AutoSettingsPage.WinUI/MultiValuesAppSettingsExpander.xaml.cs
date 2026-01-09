using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class MultiValuesAppSettingsExpander : IEntryControl<IMultiValuesSettingsEntry>
{
    public IMultiValuesSettingsEntry Entry
    {
        get;
        set
        {
            field = value;
            foreach (var entry in Entry.Entries)
                Items.Add(SettingsEntryHelper.GetControl(entry));
        }
    } = null!;

    public MultiValuesAppSettingsExpander() => InitializeComponent();
}
