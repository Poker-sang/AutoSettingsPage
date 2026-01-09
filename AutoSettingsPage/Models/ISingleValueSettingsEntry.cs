using System.Numerics;

namespace AutoSettingsPage.Models;

public interface ISingleValueSettingsEntry<TValue> : ISettingsEntry
{
    string Token { get; }

    TValue Value { get; set; }

    string? Placeholder { get; }
}

public interface IMinMaxEntry<TValue> : ISingleValueSettingsEntry<TValue>
{
    TValue Max { get; }

    TValue Min { get; }
}

public interface INumberSettingsEntry<TNumber> : IMinMaxEntry<TNumber> where TNumber : INumberBase<TNumber>
{
    TNumber Step { get; }
}

public interface IEnumSettingsEntry<TEnum> : ISingleValueSettingsEntry<TEnum>
{
    IReadOnlyList<EnumStringPair<TEnum>> EnumItems { get; }
}

public interface IOptionSettingsEntry<out TOption> : ISettingsEntry
{
    TOption Option { get; }
}


public interface IMultiValuesSettingsEntry : ISettingsEntry
{
    IReadOnlyList<ISettingsEntry> Entries { get; }
}
