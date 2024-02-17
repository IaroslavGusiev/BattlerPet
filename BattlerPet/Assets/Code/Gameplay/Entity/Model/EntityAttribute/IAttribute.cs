using UniRx;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public interface IAttribute
    {
        AttributeType AttributeType { get; }
        
        IReadOnlyReactiveProperty<float> MaxValue { get; }
        IReadOnlyReactiveProperty<float> CurrentValue { get; }
    }
}