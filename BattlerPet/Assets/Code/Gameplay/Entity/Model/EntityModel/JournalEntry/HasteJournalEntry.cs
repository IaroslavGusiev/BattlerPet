namespace Code.Gameplay.Entity
{
    public struct HasteJournalEntry : IJournalEntry
    {
        public string Timestamp { get; set; }
        public float AmountAdded { get; set; }
        public float CurrentHaste { get; set; }
    }
}