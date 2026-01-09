using AutoSettingsPage.Models;
using FluentIcons.Common;

namespace AutoSettingsPage;

public interface ISettingsBuilder<out TSettings>
{
    TSettings Settings { get; }

    ISettingsBuilder<TSettings> Add<TEntry>(TEntry entry,
        Action<TEntry>? config = null)
        where TEntry : ISettingsEntry;

    ISettingsBuilder<TSettings> NewGroup(string header, string description = "", Symbol icon = default, Uri? descriptionUri = null);

    ISettingsBuilder<TSettings> ConfigLastEntry(Action<ISettingsEntry> config);

    ISettingsBuilder<TSettings> ConfigLastGroup(Action<ISettingsGroup> config);

    IReadOnlyList<ISettingsGroup> Build();
}
