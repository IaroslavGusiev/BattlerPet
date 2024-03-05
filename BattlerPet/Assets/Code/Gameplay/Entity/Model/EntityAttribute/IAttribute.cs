using UniRx;

namespace Code.Gameplay.Entity
{
    public interface IAttribute
    {
        IReadOnlyReactiveProperty<float> MaxValue { get; }
        IReadOnlyReactiveProperty<float> CurrentValue { get; }
    }
}