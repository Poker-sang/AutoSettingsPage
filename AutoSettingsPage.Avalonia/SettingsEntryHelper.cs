using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoSettingsPage.Models;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace AutoSettingsPage.Avalonia;

public static class SettingsEntryHelper
{
    public static IReadOnlyList<string> AvailableFonts { get; set; } = [];

    public static FuncValueConverter<ISettingsEntry, object?> DescriptionConverter { get; } = new(DescriptionControl);

    public static object? DescriptionControl(ISettingsEntry? entry)
    {
        if (entry?.DescriptionUri is not null)
        {
            var b = new HyperlinkButton
            {
                Padding = new(0),
                Content = entry.Description,
                NavigateUri = entry.DescriptionUri
            };
            return b;
        }

        return entry?.Description;
    }

    public static Control GetControl(ISettingsEntry entry) => FactoryDictionary[entry.GetType()](entry);

    public static Dictionary<Type, Func<ISettingsEntry, Control>> FactoryDictionary { get; } = new();

    extension(Dictionary<Type, Func<ISettingsEntry, Control>> dictionary)
    {
        public Dictionary<Type, Func<ISettingsEntry, Control>> Add<TEntry, TControl>()
            where TEntry : ISettingsEntry
            where TControl : Control, IEntryControl<TEntry>, new()
        {
            dictionary[typeof(TEntry)] = entry => new TControl { Entry = (TEntry)entry };
            return dictionary;
        }

        public Dictionary<Type, Func<ISettingsEntry, Control>> AddPredefined<TSettings>()
        {
            return dictionary
                .Add<ClickableSettingsEntry, ClickableSettingsCard>()
                .Add<StringSettingsEntry<TSettings>, StringSettingsCard>()
                .Add<DoubleSettingsEntry<TSettings>, DoubleSettingsCard>()
                .Add<IntSettingsEntry<TSettings>, DoubleSettingsCard>()
                .Add<BoolSettingsEntry<TSettings>, BoolSettingsCard>()
                .Add<EnumSettingsEntry<TSettings, object>, EnumSettingsCard>()
                .Add<DateTimeSettingsEntry<TSettings>, DateSettingsCard>()
                .Add<FontSettingsEntry<TSettings>, FontSettingsCard>()
                .Add<ColorSettingsEntry<TSettings>, ColorSettingsCard>()
                .Add<MultiValuesEntry<TSettings>, MultiValuesSettingsExpander>()
                .Add<MultiValuesWithSwitchEntry<TSettings>, MultiValuesWithSwitchSettingsExpander>();
        }
    }

    extension<TSettings>(ISettingsGroupBuilder<TSettings> builder)
    {
        public ISettingsGroupBuilder<TSettings> Color(
            Expression<Func<TSettings, uint>> property,
            Action<ColorSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property), config);

        public ISettingsGroupBuilder<TSettings> Font(
            Expression<Func<TSettings, string>> property,
            Action<FontSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property), config);
    }
}
