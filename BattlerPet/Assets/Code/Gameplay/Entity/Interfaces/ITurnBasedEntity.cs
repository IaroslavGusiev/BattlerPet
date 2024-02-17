namespace Code.Gameplay.Entity
{
    public interface ITurnBasedEntity
    {
        bool IsReadyForTurn();
        void EnableTurnIndicator(bool enable);
    }
}