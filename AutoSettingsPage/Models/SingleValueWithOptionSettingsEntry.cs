using System.Linq.Expressions;

namespace AutoSettingsPage.Models;

public class SingleValueWithOptionSettingsEntry<TSettings, TValue, TOption>(
    TSettings settings,
    Expression<Func<TSettings, TValue>> property,
    TOption option)
    : SingleValueSettingsEntry<TSettings, TValue>(settings, property), IOptionSettingsEntry<TOption>
{
    /// <inheritdoc />
    public TOption Option { get; set; } = option;
}
