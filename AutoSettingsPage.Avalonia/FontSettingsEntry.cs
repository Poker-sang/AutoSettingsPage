using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using AutoSettingsPage.Models;
using FluentIcons.Common;

namespace AutoSettingsPage.Avalonia;

public class FontSettingsEntry<TSettings> : CollectionSettingsEntry<TSettings, string>
{
    public FontSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, ObservableCollection<string>> getter,
        Action<TSettings, ObservableCollection<string>> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
    }

    public FontSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, ObservableCollection<string>> getter,
        Action<TSettings, ObservableCollection<string>> setter)
        : base(settings, token, attribute, getter, setter)
    {
    }

    public FontSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, ObservableCollection<string>>> property)
        : base(settings, property)
    {
    }
}
