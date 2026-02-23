using AutoSettingsPage.Models;

namespace AutoSettingsPage.Avalonia;

public interface IEntryControl<in TEntry> where TEntry : ISettingsEntry
{
    TEntry Entry { set; }
}
