using System.Collections.ObjectModel;
using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class CollectionSettingsEntry<TSettings, TItem>
    : SingleValueSettingsEntry<TSettings, ObservableCollection<TItem>>
{
    public CollectionSettingsEntry(
        TSettings settings,
        string token,
        string header,
        string description,
        Symbol icon,
        string? placeholder,
        Func<TSettings, ObservableCollection<TItem>> getter,
        Action<TSettings, ObservableCollection<TItem>> setter)
        : base(settings, token, header, description, icon, placeholder, getter, setter)
    {
        Value.CollectionChanged += (_, _) => Setter(Settings, Value);
    }

    public CollectionSettingsEntry(
        TSettings settings,
        string token,
        SettingsEntryAttribute attribute,
        Func<TSettings, ObservableCollection<TItem>> getter,
        Action<TSettings, ObservableCollection<TItem>> setter)
        : base(settings, token, attribute, getter, setter)
    {
        Value.CollectionChanged += (_, _) => Setter(Settings, Value);
    }

    public CollectionSettingsEntry(
        TSettings settings,
        Expression<Func<TSettings, ObservableCollection<TItem>>> property)
        : base(settings, property)
    {
        Value.CollectionChanged += (_, _) => Setter(Settings, Value);
    }

    /// <inheritdoc />
    public sealed override ObservableCollection<TItem> Value
    {
        get => base.Value;
        set => base.Value = value;
    }
}
