using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class MultiValuesWithSwitchSettingsExpander : SettingsExpander, IEntryControl<IMultiValuesWithSwitchSettingsEntry>
{
    public IMultiValuesWithSwitchSettingsEntry Entry
    {
        set
        {
            DataContext = value;
            Items.Clear();
            foreach (var entry in value.Entries)
                Items.Add(SettingsEntryHelper.GetControl(entry));
        }
    }

    public MultiValuesWithSwitchSettingsExpander() => InitializeComponent();
}
