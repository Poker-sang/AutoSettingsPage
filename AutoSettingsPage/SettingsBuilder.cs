using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Numerics;
using AutoSettingsPage.Models;
using FluentIcons.Common;

namespace AutoSettingsPage;

public static class SettingsBuilder
{
    public static ISettingsBuilder<TSettings> Create<TSettings>(TSettings settings) =>
        new SettingsBuilderImpl<TSettings>(settings);

    extension<TSettings>(ISettingsBuilder<TSettings> builder)
    {
        public ISettingsBuilder<TSettings> Clickable(
            string token,
            string header,
            string description,
            Symbol icon,
            EventHandler<ClickableSettingsEntry, EventArgs> clicked,
            Action<ClickableSettingsEntry>? config = null) =>
            builder.Add(new(token, header, description, icon, clicked), config);
        public ISettingsBuilder<TSettings> SingleValue<TValue>(
            Expression<Func<TSettings, TValue>> property,
            Action<SingleValueSettingsEntry<TSettings, TValue>>? config = null) =>
            builder.Add(new(builder.Settings, property), config);

        public ISettingsBuilder<TSettings> SingleValueWithOption<TValue, TOption>(
            Expression<Func<TSettings, TValue>> property,
            TOption option,
            Action<SingleValueWithOptionSettingsEntry<TSettings, TValue, TOption>>? config = null) =>
            builder.Add(new(builder.Settings, property, option), config);

        public ISettingsBuilder<TSettings> String(
            Expression<Func<TSettings, string>> property,
            Action<StringSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property), config);

        public ISettingsBuilder<TSettings> Number<TNumber>(
            Expression<Func<TSettings, TNumber>> property,
            TNumber min, TNumber max, TNumber step,
            Action<NumberSettingsEntry<TSettings, TNumber>>? config = null)
            where TNumber : INumber<TNumber>, IMinMaxValue<TNumber> =>
            builder.Add(new(builder.Settings, property)
            {
                Min = min,
                Max = max,
                Step = step
            }, config);

        public ISettingsBuilder<TSettings> Int(
            Expression<Func<TSettings, int>> property,
            int min, int max, int step,
            Action<IntSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property)
            {
                Min = min,
                Max = max,
                Step = step
            }, config);

        public ISettingsBuilder<TSettings> Double(
            Expression<Func<TSettings, double>> property,
            double min, double max, double step,
            Action<DoubleSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property)
            {
                Min = min,
                Max = max,
                Step = step
            }, config);

        public ISettingsBuilder<TSettings> UInt(
            Expression<Func<TSettings, uint>> property,
            uint min, uint max, uint step,
            Action<UIntSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property)
            {
                Min = min,
                Max = max,
                Step = step
            }, config);

        public ISettingsBuilder<TSettings> DateTime(
            Expression<Func<TSettings, DateTimeOffset>> property,
            DateTimeOffset min, DateTimeOffset max,
            Action<DateTimeOffsetSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property)
            {
                Min = min,
                Max = max
            }, config);

        public ISettingsBuilder<TSettings> Bool(
            Expression<Func<TSettings, bool>> property,
            Action<BoolSettingsEntry<TSettings>>? config = null) =>
            builder.Add(new(builder.Settings, property), config);

        public ISettingsBuilder<TSettings> Enum<TEnum>(Expression<Func<TSettings, TEnum>> property,
            IReadOnlyList<EnumStringPair<TEnum>> enumItems,
            Action<EnumSettingsEntry<TSettings, TEnum>>? config = null) =>
            builder.Add(new(builder.Settings, property, enumItems), config);

        public ISettingsBuilder<TSettings> Collection<TItem>(Expression<Func<TSettings, ObservableCollection<TItem>>> property,
            Action<CollectionSettingsEntry<TSettings, TItem>>? config = null) =>
            builder.Add(new(builder.Settings, property), config);

        public ISettingsBuilder<TSettings> MultiValues(Expression<Func<TSettings, object>> property,
            Action<ISettingsBuilder<TSettings>>? configValues,
            Action<MultiValuesEntry<TSettings>>? config = null)
        {
            var simpleAddSettingsEntry = new SettingsBuilderImpl<TSettings>(builder.Settings);
            configValues?.Invoke(simpleAddSettingsEntry);
            return builder.Add(new(property, simpleAddSettingsEntry.Build()), config);
        }
    }

    private class SettingsBuilderImpl<TSettings> : ISettingsBuilder<TSettings>
    {
        public TSettings Settings { get; }

        private readonly List<SimpleSettingsGroup> _groups = [];

        private SimpleSettingsGroup CurrentGroup
        {
            get
            {
                if (_currentGroup is null)
                {
                    _currentGroup = new("", "", default);
                    _groups.Add(_currentGroup);
                }

                return _currentGroup;
            }
        }

        private SimpleSettingsGroup? _currentGroup;

        private ISettingsEntry? _currentEntry;

        internal SettingsBuilderImpl(TSettings settings) => Settings = settings;

        /// <inheritdoc />
        public ISettingsBuilder<TSettings> Add<TEntry>(TEntry entry,
            Action<TEntry>? config = null)
            where TEntry : ISettingsEntry
        {
            _currentEntry = entry;
            CurrentGroup.Add(entry);
            config?.Invoke(entry);
            return this;
        }

        /// <inheritdoc />
        public ISettingsBuilder<TSettings> NewGroup(string header, string description = "", Symbol icon = default, Uri? descriptionUri = null)
        {
            _currentGroup = new(header, description, icon, descriptionUri);
            _groups.Add(_currentGroup);
            return this;
        }

        /// <inheritdoc />
        public ISettingsBuilder<TSettings> ConfigLastEntry(Action<ISettingsEntry> config)
        {
            if (_currentEntry is null)
                throw new InvalidOperationException("No entry has been added to configure.");
            config(_currentEntry);
            return this;
        }

        /// <inheritdoc />
        public ISettingsBuilder<TSettings> ConfigLastGroup(Action<ISettingsGroup> config)
        {
            if (_currentGroup is null)
                throw new InvalidOperationException("No group has been added to configure.");
            config(_currentGroup);
            return this;
        }

        /// <inheritdoc />
        public IReadOnlyList<ISettingsGroup> Build() => _groups;
    }
}
