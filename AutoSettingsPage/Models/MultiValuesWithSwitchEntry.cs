using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class MultiValuesWithSwitchEntry<TSettings> : BoolSettingsEntry<TSettings>, IMultiValuesSettingsEntry
{
    public MultiValuesWithSwitchEntry(
        TSettings settings, 
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, bool> getter,
        Action<TSettings, bool> setter,
        IReadOnlyList<ISettingsEntry> entries) : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
        Entries = entries;
    }

    public MultiValuesWithSwitchEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, bool> getter,
        Action<TSettings, bool> setter,
        IReadOnlyList<ISettingsEntry> entries)
        : base(settings, token, attribute, getter, setter)
    {
        Entries = entries;
    }

    public MultiValuesWithSwitchEntry(TSettings settings, Expression<Func<TSettings, bool>> property, IReadOnlyList<ISettingsEntry> entries)
        : base(settings, property)
    {
        Entries = entries;
    }

    public IReadOnlyList<ISettingsEntry> Entries { get; set; }
}
