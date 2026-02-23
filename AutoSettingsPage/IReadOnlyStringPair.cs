namespace AutoSettingsPage;

public interface IReadOnlyStringPair<out TEnum>
{
    TEnum Value { get; }

    string Description { get; }
}
