using System.Linq.Expressions;

namespace AutoSettingsPage.Models;

public class DateTimeOffsetSettingsEntry<TSettings>(
    TSettings settings,
    Expression<Func<TSettings, DateTimeOffset>> property)
    : SingleValueSettingsEntry<TSettings, DateTimeOffset>(settings, property), IMinMaxEntry<DateTimeOffset>
{
    /// <inheritdoc />
    public DateTimeOffset Max { get; set; } = DateTimeOffset.MaxValue;

    /// <inheritdoc />
    public DateTimeOffset Min { get; set; } = DateTimeOffset.MinValue;
}
