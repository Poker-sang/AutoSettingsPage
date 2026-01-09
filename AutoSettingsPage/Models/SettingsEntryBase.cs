using System.Linq.Expressions;
using System.Reflection;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public abstract class SettingsEntryBase(
    string token,
    string header,
    string description,
    Symbol icon) : ISettingsEntry
{
    public string Token { get; } = token;

    public string Header { get; set; } = header;

    public string Description { get; set; } = description;

    public Symbol Icon { get; set; } = icon;

    public virtual Uri? DescriptionUri { get; set; }

    private protected static MemberExpression GetMemberExpression<TSettings, TValue>(Expression<Func<TSettings, TValue>> property)
    {
        return property.Body switch
        {
            // t => (T)t.A
            UnaryExpression { Operand: MemberExpression member1 } => member1,
            // t => t.A
            MemberExpression member2 => member2,
            _ => throw new ArgumentException(PropertyExceptionString, nameof(property))
        };
    }

    private protected static string GetInfoFromMember(
        MemberExpression member,
        out string header,
        out string description,
        out Symbol icon,
        out string? placeholder)
    {
        if (member.Member.GetCustomAttribute<SettingsEntryAttribute>() is { } attribute)
        {
            header = attribute.HeaderResource;
            description = attribute.DescriptionResource ?? "";
            icon = attribute.Symbol;
            placeholder = attribute.PlaceholderResource;
        }
        else
        {
            header = "";
            description = "";
            icon = default;
            placeholder = null;
        }

        return member.Member.Name;
    }

    private protected const string PropertyExceptionString = "The property expression is not a valid member expression.";
}
