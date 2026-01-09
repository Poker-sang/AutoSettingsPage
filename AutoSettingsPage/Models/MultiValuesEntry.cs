using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class MultiValuesEntry<TSettings>(
    string token,
    string header,
    string description,
    Symbol icon,
    IReadOnlyList<ISettingsEntry> entries)
    : SettingsEntryBase(token, header, description, icon), IMultiValuesSettingsEntry
{
    /// <summary>
    /// 只从<paramref name="property"/>取出<see cref="SettingsEntryAttribute"/>元数据
    /// </summary>
    /// <param name="property"></param>
    /// <param name="entries"></param>
    public MultiValuesEntry(Expression<Func<TSettings, object>> property, IReadOnlyList<ISettingsEntry> entries)
        : this(GetInfoFromMember(GetMemberExpression(property), out var header, out var description, out var icon, out _),
            header, description, icon, entries)
    {
    }

    public IReadOnlyList<ISettingsEntry> Entries { get; set; } = entries;
}
