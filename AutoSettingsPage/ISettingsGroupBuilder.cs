using AutoSettingsPage.Models;

namespace AutoSettingsPage;

public interface ISettingsGroupBuilder<out TSettings>
{
    TSettings Settings { get; }

    ISettingsGroupBuilder<TSettings> Add<TEntry>(
        TEntry entry,
        Action<TEntry>? config = null)
        where TEntry : ISettingsEntry;

    ISettingsGroupBuilder<TSettings> ConfigLast(Action<ISettingsEntry> config);

    IReadOnlyList<ISettingsEntry> Build();
}
