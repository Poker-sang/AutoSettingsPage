using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public interface ISettingsEntry
{
    string Token { get; }

    string Header { get; }

    string Description { get; }

    Symbol Icon { get; }

    Uri? DescriptionUri { get; }
}
