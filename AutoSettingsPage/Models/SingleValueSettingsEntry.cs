using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class SingleValueSettingsEntry<TSettings, TValue>(
    TSettings settings,
    string token,
    string header,
    string description,
    Symbol icon,
    Func<TSettings, TValue> getter,
    Action<TSettings, TValue> setter)
    : ObservableSettingsEntry(token, header, description, icon), ISingleValueSettingsEntry<TValue>
{
    public Action<TValue>? ValueChanged { get; set; }

    public TSettings Settings { get; } = settings;

    /// <inheritdoc />
    public virtual TValue Value
    {
        get => getter(Settings);
        set
        {
            if (EqualityComparer<TValue>.Default.Equals(Value, value))
                return;
            Setter(Settings, value);
            OnPropertyChanged();
            ValueChanged?.Invoke(Value);
        }
    }

    /// <inheritdoc />
    public string? Placeholder { get; set; }

    private protected readonly Action<TSettings, TValue> Setter = setter;

    public SingleValueSettingsEntry(TSettings settings,
        Expression<Func<TSettings, object>> property,
        Func<TSettings, TValue> getter,
        Action<TSettings, TValue> setter)
        : this(settings, GetSettingsEntryInfo(property, out var header, out var description, out var icon, out var placeholder),
            header, description, icon, getter, setter)
    {
        Placeholder = placeholder;
    }

    public SingleValueSettingsEntry(TSettings settings,
        Expression<Func<TSettings, TValue>> property)
        : this(settings, GetSettingsEntryInfo(property, out var header, out var description, out var icon, out var getter, out var setter, out var placeholder),
            header, description, icon, getter, setter)
    {
        Placeholder = placeholder;
    }

    protected static string GetSettingsEntryInfo(
        Expression<Func<TSettings, TValue>> property,
        out string header,
        out string description,
        out Symbol icon,
        out Func<TSettings, TValue> getter,
        out Action<TSettings, TValue> setter,
        out string? placeholder)
    {
        if (property is not { Parameters: [{ } parameter] })
            throw new ArgumentException(PropertyExceptionString, nameof(property));

        var propertyValue = Expression.Parameter(typeof(TValue));
        BinaryExpression setPropertyValue;
        Expression getPropertyValue;
        MemberExpression member;
        switch (property.Body)
        {
            // t => (T)t.A
            case UnaryExpression
            {
                Operand: MemberExpression member1
            } body:
            {
                // t => (T)t.A
                getPropertyValue = body;
                // (t, v) => t.A = (T)v
                setPropertyValue = Expression.Assign(member1, Expression.Convert(propertyValue, member1.Type));
                // t => t.A
                member = member1;
                break;
            }
            // t => t.A
            case MemberExpression member2:
            {
                // t => t.A
                getPropertyValue = member = member2;
                // (t, v) => t.A = v
                setPropertyValue = Expression.Assign(member2, propertyValue);
                break;
            }
            default:
                throw new ArgumentException(PropertyExceptionString, nameof(property));
        }

        getter = Expression.Lambda<Func<TSettings, TValue>>(getPropertyValue, parameter).Compile();
        setter = Expression.Lambda<Action<TSettings, TValue>>(setPropertyValue, parameter, propertyValue).Compile();
        return GetInfoFromMember(member, out header, out description, out icon, out placeholder);
    }

    protected static string GetSettingsEntryInfo<TValue2>(
        Expression<Func<TSettings, TValue2>> property,
        out string header,
        out string description,
        out Symbol icon,
        out string? placeholder)
    {
        var member = GetMemberExpression(property);

        return GetInfoFromMember(member, out header, out description, out icon, out placeholder);
    }
}
