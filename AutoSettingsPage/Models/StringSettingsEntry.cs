using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class StringSettingsEntry<TSettings> : SingleValueSettingsEntry<TSettings, string>
{
    public StringSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, string> getter,
        Action<TSettings, string> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public StringSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, string> getter,
        Action<TSettings, string> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public StringSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, string>> property)
        : base(settings, property)
    {
    }
}
