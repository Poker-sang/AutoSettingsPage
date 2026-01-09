using System.Linq.Expressions;

namespace AutoSettingsPage.Models;

public class BoolSettingsEntry<TSettings>(
    TSettings settings,
    Expression<Func<TSettings, bool>> property)
    : SingleValueSettingsEntry<TSettings, bool>(settings, property);
