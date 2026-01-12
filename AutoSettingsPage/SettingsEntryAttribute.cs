using FluentIcons.Common;

namespace AutoSettingsPage;

[AttributeUsage(AttributeTargets.Property)]
public class SettingsEntryAttribute : Attribute
{
    public Symbol Icon { get; init; }

    public string Header { get; init; } = "";

    public string Description { get; init; } = "";

    public string? Placeholder { get; init; }

    public static SettingsEntryAttribute Empty { get; } = new();

    public SettingsEntryAttribute()
    {
    }

    public SettingsEntryAttribute(Symbol icon, string? headerResource, string? descriptionResource, string? placeholderResource = null)
    {
        Icon = icon;
        if (headerResource is not null)
            Header = SettingsResourceKeysProvider[headerResource];
        if (descriptionResource is not null)
            Description = SettingsResourceKeysProvider[descriptionResource];
        if (placeholderResource is not null)
            Placeholder = SettingsResourceKeysProvider[placeholderResource];
    }

    public static ISettingsResourceKeysProvider SettingsResourceKeysProvider { get; set; } = SimpleSettingsResourceKeysProvider.Default;
}
