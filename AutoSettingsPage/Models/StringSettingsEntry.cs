using System.Linq.Expressions;

namespace AutoSettingsPage.Models;

public class StringSettingsEntry<TSettings>(
    TSettings settings,
    Expression<Func<TSettings, string>> property)
    : SingleValueSettingsEntry<TSettings, string>(settings, property);
