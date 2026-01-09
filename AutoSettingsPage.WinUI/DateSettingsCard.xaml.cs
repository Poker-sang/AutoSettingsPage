using System;
using AutoSettingsPage.Models;

namespace AutoSettingsPage.WinUI;

public sealed partial class DateSettingsCard : IEntryControl<ISingleValueSettingsEntry<DateTimeOffset>>
{
    public ISingleValueSettingsEntry<DateTimeOffset> Entry { get; set; } = null!;

    public DateSettingsCard() => InitializeComponent();
}
