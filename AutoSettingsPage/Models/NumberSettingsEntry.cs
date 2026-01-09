using System.Linq.Expressions;
using System.Numerics;

namespace AutoSettingsPage.Models;

public class NumberSettingsEntry<TSettings, TNumber>(
    TSettings settings,
    Expression<Func<TSettings, TNumber>> property)
    : SingleValueSettingsEntry<TSettings, TNumber>(settings, property), INumberSettingsEntry<TNumber>
    where TNumber : INumber<TNumber>, IMinMaxValue<TNumber>
{

    /// <inheritdoc />
    public TNumber Max { get; set; } = TNumber.MaxValue;

    /// <inheritdoc />
    public TNumber Min { get; set; } = TNumber.MinValue;

    /// <inheritdoc />
    public TNumber Step { get; set; } = TNumber.One;
}

public class UIntSettingsEntry<TSettings>(
    TSettings settings,
    Expression<Func<TSettings, uint>> property)
    : NumberSettingsEntry<TSettings, uint>(settings, property);

public class IntSettingsEntry<TSettings>(
    TSettings settings,
    Expression<Func<TSettings, int>> property)
    : NumberSettingsEntry<TSettings, int>(settings, property);

public class DoubleSettingsEntry<TSettings>(
    TSettings settings,
    Expression<Func<TSettings, double>> property)
    : NumberSettingsEntry<TSettings, double>(settings, property);
