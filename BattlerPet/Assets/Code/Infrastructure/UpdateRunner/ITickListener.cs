namespace Code.Infrastructure.UpdateRunner
{
    public interface ITickListener
    {
        void Tick(float deltaTime);
    }
}