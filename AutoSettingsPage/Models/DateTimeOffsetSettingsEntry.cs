using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class DateTimeOffsetSettingsEntry<TSettings> : SingleValueSettingsEntry<TSettings, DateTimeOffset>, IMinMaxEntry<DateTimeOffset>
{
    public DateTimeOffsetSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, DateTimeOffset> getter,
        Action<TSettings, DateTimeOffset> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public DateTimeOffsetSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, DateTimeOffset> getter,
        Action<TSettings, DateTimeOffset> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public DateTimeOffsetSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, DateTimeOffset>> property)
        : base(settings, property)
    {
    }

    /// <inheritdoc />
    public DateTimeOffset Max { get; set; } = DateTimeOffset.MaxValue;

    /// <inheritdoc />
    public DateTimeOffset Min { get; set; } = DateTimeOffset.MinValue;
}
