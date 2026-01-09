using System.Collections.ObjectModel;
using AutoSettingsPage.Models;
using CommunityToolkit.WinUI.Controls;

namespace AutoSettingsPage.WinUI;

public sealed partial class TokenizingSettingsExpander : IEntryControl<ISingleValueSettingsEntry<ObservableCollection<string>>>
{
    public ISingleValueSettingsEntry<ObservableCollection<string>> Entry { get; set; } = null!;

    public TokenizingSettingsExpander() => InitializeComponent();

    private void TokenizingTextBox_OnTokenItemAdding(TokenizingTextBox sender, TokenItemAddingEventArgs e)
    {
        if (Entry.Value.Contains(e.TokenText))
            e.Cancel = true;
    }
}
