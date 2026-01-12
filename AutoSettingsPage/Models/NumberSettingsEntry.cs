using System.Linq.Expressions;
using System.Numerics;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class NumberSettingsEntry<TSettings, TNumber> : SingleValueSettingsEntry<TSettings, TNumber>, INumberSettingsEntry<TNumber>
    where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>
{
    public NumberSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, TNumber> getter,
        Action<TSettings, TNumber> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public NumberSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, TNumber> getter,
        Action<TSettings, TNumber> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public NumberSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, TNumber>> property)
        : base(settings, property)
    {
    }

    /// <inheritdoc />
    public TNumber Max { get; set; } = TNumber.MaxValue;

    /// <inheritdoc />
    public TNumber Min { get; set; } = TNumber.MinValue;

    /// <inheritdoc />
    public TNumber Step { get; set; } = TNumber.One;
}

public class UIntSettingsEntry<TSettings> : NumberSettingsEntry<TSettings, uint>
{
    public UIntSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, uint> getter,
        Action<TSettings, uint> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public UIntSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, uint> getter,
        Action<TSettings, uint> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public UIntSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, uint>> property)
        : base(settings, property)
    {
    }
}

public class IntSettingsEntry<TSettings> : NumberSettingsEntry<TSettings, int>
{
    public IntSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, int> getter,
        Action<TSettings, int> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public IntSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, int> getter,
        Action<TSettings, int> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public IntSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, int>> property)
        : base(settings, property)
    {
    }
}

public class DoubleSettingsEntry<TSettings> : NumberSettingsEntry<TSettings, double>
{
    public DoubleSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, double> getter,
        Action<TSettings, double> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public DoubleSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, double> getter,
        Action<TSettings, double> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public DoubleSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, double>> property)
        : base(settings, property)
    {
    }
}
