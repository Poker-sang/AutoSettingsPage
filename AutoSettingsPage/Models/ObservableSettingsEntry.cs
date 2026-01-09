using System.ComponentModel;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public abstract class ObservableSettingsEntry(
    string token,
    string header,
    string description,
    Symbol icon)
    : SettingsEntryBase(token, header, description, icon), INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
