using UniRx;
using UnityEngine;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class EntityAttribute : IAttribute
    {
        public AttributeType AttributeType { get; }

        public IReadOnlyReactiveProperty<float> CurrentValue => _currentValue;
        public IReadOnlyReactiveProperty<float> MaxValue => _maxValue;
        
        private readonly ReactiveProperty<float> _maxValue = new();
        private readonly ReactiveProperty<float> _currentValue = new();

        public EntityAttribute(AttributeType attributeType, float maxValue, float currentValue)
        {
            AttributeType = attributeType;
            _maxValue.Value = maxValue;
            _currentValue.Value = currentValue;
        }

        public void DecreaseCurrentValue(float value) => 
            _currentValue.Value = Mathf.Max(_currentValue.Value - value, 0);

        public void IncreaseCurrentValue(float value) => 
            _currentValue.Value = Mathf.Min(_currentValue.Value + value, MaxValue.Value);
    }
}