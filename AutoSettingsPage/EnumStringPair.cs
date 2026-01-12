namespace AutoSettingsPage;

public interface IReadOnlyEnumStringPair<out TEnum> 
{
    TEnum Enum { get; }

    string DisplayString { get; }
}

public record EnumStringPair<TEnum>(TEnum Enum, string DisplayString) : IReadOnlyEnumStringPair<TEnum>
{
    /// <inheritdoc />
    public override string ToString() => DisplayString;
}
