using UniRx;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public interface IReactiveModel
    {
        // public IAttribute HasteAttribute { get; }
        // public IAttribute HealthAttribute { get;  }

        public IAttribute GetAttribute(AttributeType attributeType);
        IReadOnlyReactiveProperty<string> UponDeath { get; }
    }
}