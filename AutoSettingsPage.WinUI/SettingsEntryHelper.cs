using System;
using System.Collections.Generic;
using AutoSettingsPage.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace AutoSettingsPage.WinUI;

internal static class SettingsEntryHelper
{
    public static IReadOnlyList<string> AvailableFonts { get; set; } = [];

    public static object DescriptionControl(this ISettingsEntry entry)
    {
        if (entry.DescriptionUri is not null)
        {
            var b = new HyperlinkButton { Content = entry.Description };
            if (entry.DescriptionUri.Scheme is "http" or "https")
            {
                b.NavigateUri = entry.DescriptionUri;
                return b;
            }

            var uri = entry.DescriptionUri;
            b.Click += (_, _) => _ = Launcher.LaunchUriAsync(uri);
            return b;
        }

        return entry.Description;
    }

    public static FrameworkElement GetControl(ISettingsEntry entry) => FactoryDictionary[entry.GetType()](entry);

    public static Dictionary<Type, Func<ISettingsEntry, FrameworkElement>> FactoryDictionary { get; } = new();

    public static void AddPredefined<TSettings>()
    {
        FactoryDictionary
            .Add<ClickableSettingsEntry, ClickableSettingsCard>()
            .Add<StringSettingsEntry<TSettings>, StringSettingsCard>()
            .Add<DoubleSettingsEntry<TSettings>, DoubleSettingsCard>()
            .Add<IntSettingsEntry<TSettings>, DoubleSettingsCard>()
            .Add<BoolSettingsEntry<TSettings>, BoolSettingsCard>()
            .Add<EnumSettingsEntry<TSettings, object>, EnumSettingsCard>()
            .Add<DateTimeOffsetSettingsEntry<TSettings>, DateSettingsCard>()
            .Add<CollectionSettingsEntry<TSettings, string>, FontSettingsCard>()
            .Add<CollectionSettingsEntry<TSettings, string>, TokenizingSettingsExpander>()
            .Add<UIntSettingsEntry<TSettings>, ColorSettingsCard>()
            .Add<MultiValuesEntry<TSettings>, MultiValuesAppSettingsExpander>();
    }


    extension(Dictionary<Type, Func<ISettingsEntry, FrameworkElement>> dictionary)
    {
        public Dictionary<Type, Func<ISettingsEntry, FrameworkElement>> Add<TEntry, TControl>()
            where TEntry : ISettingsEntry
            where TControl : FrameworkElement, IEntryControl<TEntry>, new()
        {
            dictionary[typeof(TEntry)] = entry => new TControl { Entry = (TEntry) entry };
            return dictionary;
        }
    }
}
