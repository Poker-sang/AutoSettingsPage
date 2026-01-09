using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public interface IEntryControl<in TEntry> where TEntry : ISettingsEntry
{
    TEntry Entry { set; }
}
