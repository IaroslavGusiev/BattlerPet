using System;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class JournalEntryReporter
    {
        private readonly List<IJournalEntry> _journal = new();

        public IReadOnlyList<IJournalEntry> Journal => 
            _journal.AsReadOnly();

        public void AddEntry<T>(float valueOne, float valueTwo) where T : IJournalEntry, new()
        {
            var entry = CreateJournalEntry<T>();

            switch (entry)
            {
                case TakeDamageJournalEntry takeDamageEntry:
                    takeDamageEntry.DamageTaken = valueOne;
                    takeDamageEntry.CurrentHp = valueTwo;
                    break;
                
                case HasteJournalEntry hasteEntry:
                    hasteEntry.AmountAdded = valueOne;
                    hasteEntry.CurrentHaste = valueTwo;
                    break;
            }
            
            _journal.Add(entry);
        }
        
        private T CreateJournalEntry<T>() where T : IJournalEntry, new() =>
            new() { Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
    }
}