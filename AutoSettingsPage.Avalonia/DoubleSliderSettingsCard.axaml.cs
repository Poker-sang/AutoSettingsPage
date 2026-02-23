using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class DoubleSliderSettingsCard : SettingsCard, IEntryControl<INumberSettingsEntry<double>>, IEntryControl<INumberSettingsEntry<int>>
{
    public INumberSettingsEntry<double> Entry
    {
        get;
        set
        {
            if (field == value)
                return;
            if (field is IntToDoubleEntryShim shim)
                shim.Dispose();
            field = value;
            DataContext = value;
        }
    } = null!;

    INumberSettingsEntry<int> IEntryControl<INumberSettingsEntry<int>>.Entry
    {
        set => Entry = new IntToDoubleEntryShim(value);
    }

    public DoubleSliderSettingsCard() => InitializeComponent();

    ~DoubleSliderSettingsCard()
    {
        if (Entry is IntToDoubleEntryShim shim)
            shim.Dispose();
    }
}
