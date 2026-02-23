using System;
using AutoSettingsPage.Models;
using CommunityToolkit.Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public partial class DateSettingsCard : SettingsCard, IEntryControl<ISingleValueSettingsEntry<DateTime>>
{
    public ISingleValueSettingsEntry<DateTime> Entry { set => DataContext = value; }

    public DateSettingsCard() => InitializeComponent();
}
