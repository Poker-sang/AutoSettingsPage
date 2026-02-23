using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class MultiValuesSettingsExpander : SettingsExpander, IEntryControl<IMultiValuesSettingsEntry>
{
    public IMultiValuesSettingsEntry Entry
    {
        set
        {
            DataContext = value;
            Items.Clear();
            foreach (var entry in value.Entries)
                Items.Add(SettingsEntryHelper.GetControl(entry));
        }
    }

    public MultiValuesSettingsExpander() => InitializeComponent();
}
