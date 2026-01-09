namespace AutoSettingsPage;

public interface ISettingsResourceKeysProvider
{
    string this[string resourceKey] { get; }
}