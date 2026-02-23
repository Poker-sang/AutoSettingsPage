// Copyright (c) AutoSettingsPage.Avalonia.
// Licensed under the GPL-3.0 License.

using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace AutoSettingsPage.Avalonia;

public class AutoSettingsPageTheme : Styles
{
    public AutoSettingsPageTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
