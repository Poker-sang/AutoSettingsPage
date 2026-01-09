using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public interface ISettingsGroup : IReadOnlyList<ISettingsEntry>, ISettingsEntry;

internal class SimpleSettingsGroup(string header, string description, Symbol icon, Uri? descriptionUri = null)
    : List<ISettingsEntry>, ISettingsGroup
{
    /// <inheritdoc />
    public string Header { get; } = header;

    /// <inheritdoc />
    public string Description { get; } = description;

    /// <inheritdoc />
    public Symbol Icon { get; } = icon;

    /// <inheritdoc />
    public Uri? DescriptionUri { get; } = descriptionUri;
}
