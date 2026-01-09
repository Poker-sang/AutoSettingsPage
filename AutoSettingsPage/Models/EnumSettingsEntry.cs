using System.Linq.Expressions;

namespace AutoSettingsPage.Models;

public class EnumSettingsEntry<TSettings, TEnum>(
    TSettings settings,
    Expression<Func<TSettings, TEnum>> property,
    IReadOnlyList<EnumStringPair<TEnum>> enumItems)
    : SingleValueSettingsEntry<TSettings, TEnum>(settings, property), IEnumSettingsEntry<TEnum>
{
    public IReadOnlyList<EnumStringPair<TEnum>> EnumItems { get; set; } = enumItems;
}
