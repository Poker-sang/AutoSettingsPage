using System;
using System.Linq.Expressions;
using AutoSettingsPage.Models;
using FluentIcons.Common;

namespace AutoSettingsPage.WinUI;

public partial class FontSettingsEntry<TSettings> : StringSettingsEntry<TSettings>
{
    public FontSettingsEntry(
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

    public FontSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, string> getter,
        Action<TSettings, string> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public FontSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, string>> property)
        : base(settings, property)
    {
    }
}
