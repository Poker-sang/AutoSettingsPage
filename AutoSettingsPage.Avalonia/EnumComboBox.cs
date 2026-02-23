using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;

namespace AutoSettingsPage.Avalonia;

public class EnumComboBox : ComboBox
{
    /// <inheritdoc />
    protected override Type StyleKeyOverride => typeof(ComboBox);

    public static readonly StyledProperty<object?> SelectedEnumProperty =
        AvaloniaProperty.Register<EnumComboBox, object?>(nameof(SelectedEnum));

    public object? SelectedEnum
    {
        get => GetValue(SelectedEnumProperty);
        set => SetValue(SelectedEnumProperty, value);
    }

    public new event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    public bool RaiseEventAfterLoaded { get; set; } = true;

    private bool _suppressSelectionChanged;

    static EnumComboBox()
    {
    }

    public EnumComboBox()
    {
        base.SelectionChanged += ComboBox_SelectionChanged;
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if(e.Property == SelectedEnumProperty)
        {
            if (_suppressSelectionChanged)
                return;

            _suppressSelectionChanged = true;
            SelectedItem = (ItemsSource as IEnumerable<IReadOnlyStringPair<object>>)?
                .FirstOrDefault(r => Equals(r.Value, SelectedEnum));
            _suppressSelectionChanged = false;
        }
        else if (e.Property == ItemsSourceProperty)
        {
            if (ItemsSource is IEnumerable<IReadOnlyStringPair<object>> enumerable)
            {
                SelectedItem = enumerable.FirstOrDefault();
            }
        }
    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_suppressSelectionChanged)
            return;

        var newEnum = SelectedItem is IReadOnlyStringPair<object> { Value: { } option } ? option : null;
        if (Equals(newEnum, SelectedEnum))
            return;

        _suppressSelectionChanged = true;
        SelectedEnum = newEnum;
        _suppressSelectionChanged = false;

        if (RaiseEventAfterLoaded && !IsLoaded)
            return;
        SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(
            e.RoutedEvent!,
            Array.Empty<object>(),
            Array.Empty<object>()));
    }

    public bool ItemSelected => SelectedItem is not null;

    public T GetSelectedItem<T>() where T : Enum =>
        SelectedEnum is T t ? t : throw new InvalidCastException(nameof(T));
}
