using System;
using AutoSettingsPage.Models;
using Avalonia.Interactivity;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class ClickableSettingsCard : SettingsCard, IEntryControl<ClickableSettingsEntry>
{
    public ClickableSettingsEntry Entry { set => DataContext = value; }

    public ClickableSettingsCard() => InitializeComponent();

    private void Clicked(object? sender, RoutedEventArgs e) => (DataContext as ClickableSettingsEntry)?.OnClicked(EventArgs.Empty);
}
