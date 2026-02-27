using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace CommunityToolkit.Avalonia.Controls;

/// <summary>
/// The SettingsExpander is a collapsible control to host multiple SettingsCards.
/// </summary>
public class SettingsExpander : ContentControl
{
    /// <inheritdoc />
    protected override Type StyleKeyOverride => typeof(SettingsExpander);

    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<SettingsExpander, object?>(nameof(Header));

    public static readonly StyledProperty<object?> DescriptionProperty =
        AvaloniaProperty.Register<SettingsExpander, object?>(nameof(Description));

    public static readonly StyledProperty<object?> HeaderIconProperty =
        AvaloniaProperty.Register<SettingsExpander, object?>(nameof(HeaderIcon));

    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<SettingsExpander, bool>(nameof(IsExpanded));

    public static readonly StyledProperty<Control?> ItemsHeaderProperty =
        AvaloniaProperty.Register<SettingsExpander, Control?>(nameof(ItemsHeader));

    public static readonly StyledProperty<Control?> ItemsFooterProperty =
        AvaloniaProperty.Register<SettingsExpander, Control?>(nameof(ItemsFooter));

    public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
        AvaloniaProperty.Register<SettingsExpander, IEnumerable?>(nameof(ItemsSource));

    public static readonly StyledProperty<IDataTemplate?> ItemTemplateProperty =
        AvaloniaProperty.Register<SettingsExpander, IDataTemplate?>(nameof(ItemTemplate));

    /// <summary>
    /// Gets or sets the Header.
    /// </summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the HeaderIcon.
    /// </summary>
    public object? HeaderIcon
    {
        get => GetValue(HeaderIconProperty);
        set => SetValue(HeaderIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the IsExpanded state.
    /// </summary>
    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    /// <summary>
    /// Gets or sets the ItemsHeader.
    /// </summary>
    public Control? ItemsHeader
    {
        get => GetValue(ItemsHeaderProperty);
        set => SetValue(ItemsHeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the ItemsFooter.
    /// </summary>
    public Control? ItemsFooter
    {
        get => GetValue(ItemsFooterProperty);
        set => SetValue(ItemsFooterProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source.
    /// </summary>
    public IEnumerable? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the item template.
    /// </summary>
    public IDataTemplate? ItemTemplate
    {
        get => GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the collection of child items.
    /// </summary>
    public IList<object> Items { get; set; } = new List<object>();

    /// <summary>
    /// Fires when the SettingsExpander is opened.
    /// </summary>
    public event EventHandler? Expanded;

    /// <summary>
    /// Fires when the expander is closed.
    /// </summary>
    public event EventHandler? Collapsed;

    private ItemsControl? _itemsControl;

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == IsExpandedProperty)
        {
            var newValue = e.GetNewValue<bool>();
            if (newValue)
                Expanded?.Invoke(this, EventArgs.Empty);
            else
                Collapsed?.Invoke(this, EventArgs.Empty);
        }
        else if (e.Property == ItemsSourceProperty)
        {
            UpdateItemsSource();
        }
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _itemsControl = e.NameScope.Find<ItemsControl>("PART_ItemsControl");
        UpdateItemsSource();
    }

    private void UpdateItemsSource()
    {
        _itemsControl?.ItemsSource = ItemsSource ?? Items;
    }
}
