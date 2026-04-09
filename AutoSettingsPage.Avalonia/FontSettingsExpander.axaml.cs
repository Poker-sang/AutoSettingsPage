using System;
using AutoSettingsPage.Models;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class FontSettingsExpander : SettingsExpander, IEntryControl<ISingleValueSettingsEntry<string>>
{
    public ISingleValueSettingsEntry<string> Entry
    {
        set
        {
            DataContext = value;
            ComboBox.SelectedItem = new FontFamily(value.Value.Split(',', 2, StringSplitOptions.TrimEntries)[0]);
        }
    }

    public FontSettingsExpander() => InitializeComponent();

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems is not [FontFamily fontFamily])
            return;
        if (string.IsNullOrWhiteSpace(TextBox.Text))
            TextBox.Text = fontFamily.Name;
        else
            TextBox.Text += ", " + fontFamily.Name;
    }
}
