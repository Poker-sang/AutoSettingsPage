using FluentIcons.Common;

namespace AutoSettingsPage;

[AttributeUsage(AttributeTargets.Property)]
public class SettingsEntryAttribute(Symbol symbol, string headerResource, string? descriptionResource, string? placeholderResource = null) : Attribute
{
    public Symbol Symbol { get; } = symbol;

    public string HeaderResource { get; } = SettingsResourceKeysProvider[headerResource];

    public string? DescriptionResource { get; } =
        descriptionResource is null
            ? null
            : SettingsResourceKeysProvider[descriptionResource];

    public string? PlaceholderResource { get; } =
        placeholderResource is null
            ? null
            : SettingsResourceKeysProvider[placeholderResource];

    public static ISettingsResourceKeysProvider SettingsResourceKeysProvider { get; set; } = SimpleSettingsResourceKeysProvider.Default;
}
