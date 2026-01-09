using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace AutoSettingsPage.Models;

public sealed class CollectionSettingsEntry<TSettings, TItem>
    : SingleValueSettingsEntry<TSettings, ObservableCollection<TItem>>
{
    public CollectionSettingsEntry(TSettings settings,
        Expression<Func<TSettings, ObservableCollection<TItem>>> property) : base(settings, property)
    {
        Value.CollectionChanged += (_, _) => Setter(Settings, Value);
    }
}
