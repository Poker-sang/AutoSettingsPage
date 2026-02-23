using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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

    private const string PartHeaderIconPresenterHolder = "PART_HeaderIconPresenterHolder";
    private const string PartHeaderPresenter = "PART_HeaderPresenter";
    private const string PartDescriptionPresenter = "PART_DescriptionPresenter";
    private const string PartActionIconPresenterHolder = "PART_ActionIconPresenterHolder";
    private const string PartContentPresenter = "PART_ContentPresenter";

    private Control? _headerIconHolder;
    private Control? _headerPresenter;
    private Control? _descriptionPresenter;
    private Control? _actionIconHolder;
    private Control? _contentPresenter;

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
    /// Gets or sets the icon that is shown when IsClickEnabled is set to true.
    /// </summary>
    public object? ActionIcon
    {
        get => GetValue(ActionIconProperty);
        set => SetValue(ActionIconProperty, value);
    }

    /// <summary>
    /// Gets or sets the tooltip of the ActionIcon.
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
    /// Gets or sets if the ActionIcon is shown.
    /// </summary>
    public bool IsActionIconVisible
    {
        get => GetValue(IsActionIconVisibleProperty);
        set => SetValue(IsActionIconVisibleProperty, value);
    }

    static SettingsCard()
    {
        HeaderProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.OnHeaderChanged());
        DescriptionProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.OnDescriptionChanged());
        HeaderIconProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.OnHeaderIconChanged());
        IsClickEnabledProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.OnIsClickEnabledChanged());
        IsActionIconVisibleProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.OnActionIconChanged());
        ContentProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.OnContentChanged());
        IsEnabledProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.UpdateVisualStates());
        ContentAlignmentProperty.Changed.AddClassHandler<SettingsCard>((s, _) => s.UpdateContentAlignmentStates());
    }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _headerIconHolder = e.NameScope.Find<Control>(PartHeaderIconPresenterHolder);
        _headerPresenter = e.NameScope.Find<Control>(PartHeaderPresenter);
        _descriptionPresenter = e.NameScope.Find<Control>(PartDescriptionPresenter);
        _actionIconHolder = e.NameScope.Find<Control>(PartActionIconPresenterHolder);
        _contentPresenter = e.NameScope.Find<Control>(PartContentPresenter);
        OnActionIconChanged();
        OnHeaderChanged();
        OnHeaderIconChanged();
        OnDescriptionChanged();
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
        PseudoClasses.Set(":left", ContentAlignment == ContentAlignment.Left);
        PseudoClasses.Set(":vertical", ContentAlignment == ContentAlignment.Vertical);
        UpdateContentSpacingState();
    }

    private void OnIsClickEnabledChanged()
    {
        OnActionIconChanged();
        PseudoClasses.Set(":clickable", IsClickEnabled);
    }

    private void OnActionIconChanged()
    {
        if (_actionIconHolder is not null)
        {
            _actionIconHolder.IsVisible = IsClickEnabled && IsActionIconVisible;
        }
    }

    private void OnHeaderIconChanged()
    {
        if (_headerIconHolder is not null)
        {
            _headerIconHolder.IsVisible = HeaderIcon is not null;
        }
    }

    private void OnDescriptionChanged()
    {
        if (_descriptionPresenter is not null)
        {
            _descriptionPresenter.IsVisible = !IsNullOrEmptyString(Description);
        }
    }

    private void OnHeaderChanged()
    {
        if (_headerPresenter is not null)
        {
            _headerPresenter.IsVisible = !IsNullOrEmptyString(Header);
        }
    }

    private void OnContentChanged()
    {
        if (_contentPresenter is not null)
        {
            _contentPresenter.IsVisible = Content is not null;
        }
        UpdateContentSpacingState();
    }

    private void UpdateContentSpacingState()
    {
        // On state change, checking if the Content should be wrapped (e.g. when the card is made smaller or the ContentAlignment is set to Vertical).
        // If the Content and the Header or Description are not null, we add spacing between the Content and the Header/Description.
        var hasSpacing = Content is not null
            && (!IsNullOrEmptyString(Header) || !IsNullOrEmptyString(Description))
            && ContentAlignment == ContentAlignment.Vertical;
        PseudoClasses.Set(":contentspacing", hasSpacing);
    }

    /// <inheritdoc />
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (IsClickEnabled)
        {
            PseudoClasses.Set(":pressed", true);
        }
    }

    /// <inheritdoc />
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        if (IsClickEnabled)
        {
            PseudoClasses.Set(":pressed", false);
            var args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }
    }

    /// <inheritdoc />
    protected override void OnPointerEntered(PointerEventArgs e)
    {
        base.OnPointerEntered(e);
        if (IsClickEnabled)
        {
            PseudoClasses.Set(":pointerover", true);
        }
    }

    /// <inheritdoc />
    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        if (IsClickEnabled)
        {
            PseudoClasses.Set(":pointerover", false);
            PseudoClasses.Set(":pressed", false);
        }
    }

    private static bool IsNullOrEmptyString(object? obj)
    {
        if (obj is null)
            return true;
        if (obj is string s && s.Length == 0)
            return true;
        return false;
    }
}
