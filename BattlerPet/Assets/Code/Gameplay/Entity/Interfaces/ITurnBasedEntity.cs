namespace Code.Gameplay.Entity
{
    public interface ITurnBasedEntity
    {
        bool IsReadyForTurn();
        void ResetHasteToZero();
        void EnableTurnIndicator(bool enable);
        bool IsReadyForNextTick(float hasteTickValue);
    }
}