using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public interface ISettingsEntry
{
    string Header { get; }

    string Description { get; }

    Symbol Icon { get; }

    Uri? DescriptionUri { get; }
}
