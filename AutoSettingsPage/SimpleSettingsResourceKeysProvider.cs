namespace AutoSettingsPage;

public class SimpleSettingsResourceKeysProvider(Func<string, string> provider) : ISettingsResourceKeysProvider
{
    public static SimpleSettingsResourceKeysProvider Default { get; } = new(x => x);

    /// <inheritdoc />
    public string this[string resourceKey] => provider(resourceKey);
}