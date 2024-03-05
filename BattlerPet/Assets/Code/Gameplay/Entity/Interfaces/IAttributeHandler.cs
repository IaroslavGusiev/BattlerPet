namespace Code.Gameplay.Entity
{
    public interface IAttributeHandler
    {
        void IncreaseHaste(float value);
        void ReduceHaste(float value);
        
        void IncreaseHealth(float value);
        void ReduceHealth(float value);
    }
}