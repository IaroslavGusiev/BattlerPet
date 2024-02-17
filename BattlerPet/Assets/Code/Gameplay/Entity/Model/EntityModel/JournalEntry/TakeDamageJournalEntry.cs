namespace Code.Gameplay.Entity
{
    public struct TakeDamageJournalEntry : IJournalEntry
    {
        public string Timestamp { get; set; }
        public float DamageTaken { get; set; }
        public float CurrentHp { get; set; }
    }
}