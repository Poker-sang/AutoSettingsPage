using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class DoubleSliderSettingsCard : IEntryControl<INumberSettingsEntry<double>>, IEntryControl<INumberSettingsEntry<int>>
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
        }
    } = null!;

    public DoubleSliderSettingsCard() => InitializeComponent();

    /// <inheritdoc />
    INumberSettingsEntry<int> IEntryControl<INumberSettingsEntry<int>>.Entry
    {
        set => Entry = new IntToDoubleEntryShim(value);
    }

    /// <remarks>
    /// 触发Dispose
    /// </remarks>
    ~DoubleSliderSettingsCard() => Entry = null!;
}
