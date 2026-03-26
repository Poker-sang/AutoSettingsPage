using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace CommunityToolkit.Avalonia.Controls;

/// <summary>
/// This is the base control to create consistent settings experiences, inline with the Windows 11 design language.
/// A SettingsCard can also be hosted within a SettingsExpander.
/// </summary>
public class SettingsCard : ContentControl
{
    /// <inheritdoc />
    protected override Type StyleKeyOverride => typeof(SettingsCard);

    public static readonly StyledProperty<object?> HeaderProperty =
        AvaloniaProperty.Register<SettingsCard, object?>(nameof(Header));

    public static readonly StyledProperty<object?> DescriptionProperty =
        AvaloniaProperty.Register<SettingsCard, object?>(nameof(Description));

    public static readonly StyledProperty<object?> HeaderIconProperty =
        AvaloniaProperty.Register<SettingsCard, object?>(nameof(HeaderIcon));

    public static readonly StyledProperty<object?> ActionIconProperty =
        AvaloniaProperty.Register<SettingsCard, object?>(nameof(ActionIcon));

    public static readonly StyledProperty<string?> ActionIconToolTipProperty =
        AvaloniaProperty.Register<SettingsCard, string?>(nameof(ActionIconToolTip));

    public static readonly StyledProperty<bool> IsClickEnabledProperty =
        AvaloniaProperty.Register<SettingsCard, bool>(nameof(IsClickEnabled));

    public static readonly StyledProperty<ContentAlignment> ContentAlignmentProperty =
        AvaloniaProperty.Register<SettingsCard, ContentAlignment>(nameof(ContentAlignment), ContentAlignment.Right);

    public static readonly StyledProperty<bool> IsActionIconVisibleProperty =
        AvaloniaProperty.Register<SettingsCard, bool>(nameof(IsActionIconVisible), true);

    public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
        RoutedEvent.Register<SettingsCard, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

    public event EventHandler<RoutedEventArgs>? Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    /// <summary>
    /// Gets or sets the Header.
    /// </summary>
    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public object? Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon on the left.
    /// </summary>
    public object? HeaderIcon
    {
        get => GetValue(HeaderIconProperty);
        set => SetValue(HeaderIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the icon that is shown when <see cref="IsClickEnabled"/> is set to true.
    /// </summary>
    public object? ActionIcon
    {
        get => GetValue(ActionIconProperty);
        set => SetValue(ActionIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the tooltip of the <see cref="ActionIcon"/>.
    /// </summary>
    public string? ActionIconToolTip
    {
        get => GetValue(ActionIconToolTipProperty);
        set => SetValue(ActionIconToolTipProperty, value);
    }

    /// <summary>
    /// Gets or sets if the card can be clicked.
    /// </summary>
    public bool IsClickEnabled
    {
        get => GetValue(IsClickEnabledProperty);
        set => SetValue(IsClickEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the alignment of the Content.
    /// </summary>
    public ContentAlignment ContentAlignment
    {
        get => GetValue(ContentAlignmentProperty);
        set => SetValue(ContentAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets if the <see cref="ActionIcon"/> is shown.
    /// </summary>
    public bool IsActionIconVisible
    {
        get => GetValue(IsActionIconVisibleProperty);
        set => SetValue(IsActionIconVisibleProperty, value);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == IsClickEnabledProperty)
            OnIsClickEnabledChanged();
        else if (e.Property == ContentProperty)
            OnContentChanged();
        else if (e.Property == IsEnabledProperty)
            UpdateVisualStates();
        else if (e.Property == ContentAlignmentProperty)
            UpdateContentAlignmentStates();
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        OnIsClickEnabledChanged();
        OnContentChanged();
        UpdateVisualStates();
        UpdateContentAlignmentStates();
    }

    private void UpdateVisualStates()
    {
        PseudoClasses.Set(":disabled", !IsEnabled);
    }

    private void UpdateContentAlignmentStates()
    {
        PseudoClasses.Set(":left", ContentAlignment is ContentAlignment.Left);
        PseudoClasses.Set(":vertical", ContentAlignment is ContentAlignment.Vertical);
        UpdateContentSpacingState();
    }

    private void OnIsClickEnabledChanged()
    {
        PseudoClasses.Set(":clickable", IsClickEnabled);
    }

    private void OnContentChanged()
    {
        UpdateContentSpacingState();
    }

    private void UpdateContentSpacingState()
    {
        // On state change, checking if the Content should be wrapped (e.g. when the card is made smaller or the ContentAlignment is set to Vertical).
        // If the Content and the Header or Description are not null, we add spacing between the Content and the Header/Description.
        var hasSpacing = Content is not null
            && (IsNotNullOrEmptyString(Header) || IsNotNullOrEmptyString(Description))
            && ContentAlignment is ContentAlignment.Vertical;
        PseudoClasses.Set(":content-spacing", hasSpacing);
    }

    /// <inheritdoc />
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        PseudoClasses.Set(":pressed", true);
    }

    /// <inheritdoc />
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        PseudoClasses.Set(":pressed", false);

        if (e.InitialPressMouseButton is MouseButton.Left)
        {
            var args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }
    }

    /// <inheritdoc />
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        PseudoClasses.Set(":pressed", false);
    }

    private static bool IsNotNullOrEmptyString(object? obj) => obj is not null and not "";

    public static FuncValueConverter<object?, bool> ToBoolean { get; } = new(IsNotNullOrEmptyString);
}
