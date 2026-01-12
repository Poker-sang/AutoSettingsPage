using System.Linq.Expressions;
using FluentIcons.Common;

namespace AutoSettingsPage.Models;

public class ClickableSettingsEntry : SettingsEntryBase
{
    public ClickableSettingsEntry(string token,
        string header,
        string description,
        Symbol icon,
        EventHandler<ClickableSettingsEntry, EventArgs> clicked) : base(token, header, description, icon)
    {
        Clicked = clicked;
    }

    public ClickableSettingsEntry(string token, SettingsEntryAttribute attribute, EventHandler<ClickableSettingsEntry, EventArgs> clicked)
        : base(token, attribute)
    {
        Clicked = clicked;
    }

    public ClickableSettingsEntry(LambdaExpression propertyExpression, EventHandler<ClickableSettingsEntry, EventArgs> clicked)
        : base(propertyExpression)
    {
        Clicked = clicked;
    }

    public event EventHandler<ClickableSettingsEntry, EventArgs> Clicked;

    public void OnClicked(EventArgs args) => Clicked(this, args);

    public Symbol ActionIcon { get; set; } = Symbol.Open;
}
