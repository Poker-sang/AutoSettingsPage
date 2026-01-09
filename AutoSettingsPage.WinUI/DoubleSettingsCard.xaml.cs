using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoSettingsPage.Models;
using FluentIcons.Common;

namespace AutoSettingsPage.WinUI;

public sealed partial class DoubleSettingsCard : IEntryControl<INumberSettingsEntry<double>>, IEntryControl<INumberSettingsEntry<int>>
{
    public INumberSettingsEntry<double> Entry
    {
        get => field;
        set
        {
            if (field == value)
                return;
            if (field is EntryShim shim)
                shim.Dispose();
            field = value;
        }
    } = null!;

    public DoubleSettingsCard() => InitializeComponent();

    /// <inheritdoc />
    INumberSettingsEntry<int> IEntryControl<INumberSettingsEntry<int>>.Entry
    {
        set => Entry = new EntryShim(value);
    }

    ~DoubleSettingsCard()
    {
        Entry = null!;
    }

    private class EntryShim : INumberSettingsEntry<double>, INotifyPropertyChanged, IDisposable
    {
        private readonly INumberSettingsEntry<int> _entry;

        public EntryShim(INumberSettingsEntry<int> entry)
        {
            if (entry is INotifyPropertyChanged npc)
                npc.PropertyChanged += OnPropertyChanged;
            _entry = entry;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

        /// <inheritdoc />
        public string Header => _entry.Header;

        /// <inheritdoc />
        public string Description => _entry.Description;

        /// <inheritdoc />
        public Symbol Icon => _entry.Icon;

        /// <inheritdoc />
        public Uri? DescriptionUri => _entry.DescriptionUri;

        /// <inheritdoc />
        public string Token => _entry.Token;

        /// <inheritdoc />
        public double Value
        {
            get => _entry.Value;
            set => _entry.Value = (int) value;
        }

        /// <inheritdoc />
        public string? Placeholder => _entry.Placeholder;

        /// <inheritdoc />
        public double Max => _entry.Max;

        /// <inheritdoc />
        public double Min => _entry.Min;

        /// <inheritdoc />
        public double Step => _entry.Step;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new(propertyName));

        /// <inheritdoc />
        public void Dispose()
        {
            if (_entry is INotifyPropertyChanged npc)
                npc.PropertyChanged -= OnPropertyChanged;
        }
    }
}
