using UniRx;
using LitMotion;
using UnityEngine;
using UnityEngine.UI;
using Code.StaticData.Gameplay;
using CodeBase.Extensions;

namespace Code.Gameplay.Entity
{
    public class AttributeSlider : MonoBehaviour
    {
        private const float FillTweenDuration = 0.45f;

        [field: SerializeField] public AttributeType AttributeType { get; private set; }
        [SerializeField] private Slider _slider;

        private readonly CompositeDisposable _disposables = new();
        private IAttribute _attribute;
        private MotionHandle _sliderTweenHandle;

        private void OnDestroy()
        {
            _disposables.ForEach(x => x.Dispose());
            _disposables.Clear();
        }

        public void Initialize(IAttribute attribute)
        {
            _attribute = attribute;

            _attribute.MaxValue
                .Subscribe(SetMaxValue)
                .AddTo(_disposables);

            _attribute.CurrentValue
                .Subscribe(UpdateCurrentValue)
                .AddTo(_disposables);
        }

        private void SetMaxValue(float maxValue) =>
            _slider.maxValue = maxValue;

        private void UpdateCurrentValue(float value) =>
            TweenSliderValue(_slider, value, FillTweenDuration);

        private void TweenSliderValue(Slider slider, float endValue, float duration)
        {
            _sliderTweenHandle.CancelIfValid();

            _sliderTweenHandle = LMotion
                .Create(slider.value, endValue, duration)
                .BindToSlider(slider);
        }
    }
}