using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class MultiValuesSettingsExpander : IEntryControl<IMultiValuesSettingsEntry>
{
    public IMultiValuesSettingsEntry Entry
    {
        get;
        set
        {
            field = value;
            Items.Clear();
            foreach (var entry in Entry.Entries)
                Items.Add(SettingsEntryHelper.GetControl(entry));
        }
    } = null!;

    public MultiValuesSettingsExpander() => InitializeComponent();
}
