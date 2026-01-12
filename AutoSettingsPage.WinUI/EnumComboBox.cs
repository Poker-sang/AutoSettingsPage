using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AutoSettingsPage.WinUI;

public sealed partial class EnumComboBox : ComboBox
{
    public static readonly DependencyProperty SelectedEnumProperty = DependencyProperty.Register(
        nameof(SelectedEnum), typeof(object), typeof(EnumComboBox), new(null, OnSelectedEnumPropertyChanged));

    public object? SelectedEnum
    {
        get => (object?) GetValue(SelectedEnumProperty);
        set => SetValue(SelectedEnumProperty, value);
    }

    public new event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    public EnumComboBox()
    {
        Style = Application.Current.Resources["DefaultComboBoxStyle"] as Style;
        base.SelectionChanged += ComboBox_SelectionChanged;
        var token = RegisterPropertyChangedCallback(ItemsSourceProperty, (sender, _) =>
        {
            if (sender is EnumComboBox { ItemsSource: IEnumerable<IReadOnlyEnumStringPair<object>> enumerable } box)
                box.SelectedItem = enumerable.FirstOrDefault();
        });
        Unloaded += (sender, _) => ((DependencyObject) sender).UnregisterPropertyChangedCallback(ItemsSourceProperty, token);
    }

    public bool RaiseEventAfterLoaded { get; set; } = true;

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var newEnum = SelectedItem is IReadOnlyEnumStringPair<object> { Enum: { } option } ? option : null;
        if (Equals(newEnum, SelectedEnum))
            return;
        SelectedEnum = newEnum;
        if (RaiseEventAfterLoaded && !IsLoaded)
            return;
        SelectionChanged?.Invoke(this, new([], []));
    }

    public bool ItemSelected => SelectedItem is not null;

    public T GetSelectedItem<T>() where T : Enum => SelectedEnum is T t ? t : throw new InvalidCastException(nameof(T));

    private static void OnSelectedEnumPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        if (o is EnumComboBox s)
            s.SelectedItem = (s.ItemsSource as IEnumerable<IReadOnlyEnumStringPair<object>>)?.FirstOrDefault(r => Equals(r.Enum, s.SelectedEnum));
    }
}
