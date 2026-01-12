using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class EnumSettingsEntry<TSettings, TEnum> : SingleValueSettingsEntry<TSettings, TEnum>, IEnumSettingsEntry<TEnum>
{
    public IReadOnlyList<IReadOnlyEnumStringPair<TEnum>> EnumItems { get; set; }

    public EnumSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, TEnum> getter,
        Action<TSettings, TEnum> setter,
        IReadOnlyList<IReadOnlyEnumStringPair<TEnum>> enumItems)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
        EnumItems = enumItems;
    }

    public EnumSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, TEnum> getter,
        Action<TSettings, TEnum> setter,
        IReadOnlyList<IReadOnlyEnumStringPair<TEnum>> enumItems)
        : base(settings, token, attribute, getter, setter)
    {
        EnumItems = enumItems;
    }

    public EnumSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, TEnum>> property,
        IReadOnlyList<IReadOnlyEnumStringPair<TEnum>> enumItems)
        : base(settings, property)
    {
        EnumItems = enumItems;
    }
}
