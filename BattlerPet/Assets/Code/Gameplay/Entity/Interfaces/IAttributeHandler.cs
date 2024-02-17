namespace Code.Gameplay.Entity
{
    public interface IAttributeHandler
    {
        void ReplenishHaste(float amountToAdd);
        void TakeDamage(float incomeDamage);
    }
}