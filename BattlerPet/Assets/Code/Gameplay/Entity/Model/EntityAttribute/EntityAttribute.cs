using System;
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
        
        public void ModifyCurrentValue(float value, AttributeOperation operation)
        {
            switch (operation)
            {
                case AttributeOperation.Increase:
                    IncreaseCurrentValue(value);
                    break;
                case AttributeOperation.Decrease:
                    DecreaseCurrentValue(value);
                    break;
                case AttributeOperation.Set:
                    SetToZero();
                    break;
                default:
                    ThrowArgumentOutOfRangeException(operation);
                    break;
            }
        }

        private void DecreaseCurrentValue(float value) => 
            _currentValue.Value = Mathf.Max(_currentValue.Value - value, 0);

        private void IncreaseCurrentValue(float value) => 
            _currentValue.Value = Mathf.Min(_currentValue.Value + value, MaxValue.Value);

        private void SetToZero() => 
            _currentValue.Value = 0f;

        private void ThrowArgumentOutOfRangeException(AttributeOperation operation) => 
            throw new ArgumentOutOfRangeException(nameof(operation), operation, $"Unsupported operation: {operation}. Supported operations are Increase, Decrease, and Set.");
    }
}