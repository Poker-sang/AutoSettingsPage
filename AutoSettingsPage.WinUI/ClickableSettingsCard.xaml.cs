using System;
using AutoSettingsPage.Models;
using Microsoft.UI.Xaml;

namespace AutoSettingsPage.WinUI;

public sealed partial class ClickableSettingsCard : IEntryControl<ClickableSettingsEntry>
{
    public ClickableSettingsEntry Entry { get; set; } = null!;

    public ClickableSettingsCard() => InitializeComponent();

    private void Clicked(object sender, RoutedEventArgs e) => Entry.OnClicked(EventArgs.Empty);
}
