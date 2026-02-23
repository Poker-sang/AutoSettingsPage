// Copyright (c) AutoSettingsPage.
// Licensed under the GPL-3.0 License.

using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class DateTimeSettingsEntry<TSettings> : SingleValueSettingsEntry<TSettings, DateTime>, IMinMaxEntry<DateTime>
{
    public DateTimeSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, DateTime> getter,
        Action<TSettings, DateTime> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public DateTimeSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, DateTime> getter,
        Action<TSettings, DateTime> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public DateTimeSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, DateTime>> property)
        : base(settings, property)
    {
    }

    /// <inheritdoc />
    public DateTime Max { get; set; } = DateTime.MaxValue;

    /// <inheritdoc />
    public DateTime Min { get; set; } = DateTime.MinValue;
}
