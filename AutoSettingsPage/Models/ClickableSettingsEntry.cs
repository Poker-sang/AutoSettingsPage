using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class ClickableSettingsEntry(
    string token,
    string header,
    string description,
    Symbol icon,
    EventHandler<ClickableSettingsEntry, EventArgs> clicked)
    : SettingsEntryBase(token, header, description, icon)
{
    public event EventHandler<ClickableSettingsEntry, EventArgs> Clicked = clicked;

    public void OnClicked(EventArgs args) => Clicked?.Invoke(this, args);

    public Symbol ActionIcon { get; set; } = Symbol.Open;
}
