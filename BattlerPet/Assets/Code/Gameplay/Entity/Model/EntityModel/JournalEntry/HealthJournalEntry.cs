using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public struct HealthJournalEntry : IJournalEntry
    {
        public string Timestamp { get; set; }
        public AttributeOperation AttributeOperation { get; set; }
        public float Value { get; set; }
        public float CurrentValue { get; set; }
    }
}