using AutoSettingsPage.Models;
using FluentIcons.Common;

namespace AutoSettingsPage;

public interface ISettingsGroupListBuilder<out TSettings>
{
    TSettings Settings { get; }

    ISettingsGroupListBuilder<TSettings> NewGroup(
        string header,
        string description = "",
        Symbol icon = default,
        Uri? descriptionUri = null,
        string? token = null,
        Action<ISettingsGroup>? config = null);

    ISettingsGroupListBuilder<TSettings> Config(Action<ISettingsGroupBuilder<TSettings>> config);

    IReadOnlyList<ISettingsGroup> Build();
}
