using System;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class JournalEntryReporter
    {
        private readonly List<IJournalEntry> _journal = new();

        public IReadOnlyList<IJournalEntry> Journal =>
            _journal.AsReadOnly();

        public void AddEntry(AttributeType attributeType, AttributeOperation operation, float value, float currentValue)
        {
            IJournalEntry entry = attributeType switch
            {
                AttributeType.Health => CreateJournalEntry<HealthJournalEntry>(value, currentValue, operation),
                AttributeType.Haste => CreateJournalEntry<HasteJournalEntry>(value, currentValue, operation),
                _ => null
            };
            _journal.Add(entry);
        }

        private T CreateJournalEntry<T>(float value, float currentValue, AttributeOperation operation) where T : IJournalEntry, new()
        {
            return new T
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Value = value,
                CurrentValue = currentValue,
                AttributeOperation = operation
            };
            
        }
    }
}