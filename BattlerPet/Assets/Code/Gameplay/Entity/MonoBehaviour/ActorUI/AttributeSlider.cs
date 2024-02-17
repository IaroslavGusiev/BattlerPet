using UniRx;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class AttributeSlider : MonoBehaviour
    {
        private const float FillTweenDuration = 0.45f;

        [field: SerializeField] public AttributeType AttributeType { get; private set; }
        [SerializeField] private Slider _slider;
        
        private readonly CompositeDisposable _disposables = new();
        private IAttribute _attribute;
        private Tween _tween;

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
            DoValue(_slider, value, FillTweenDuration);

        private void DoValue(Slider target, float endValue, float duration) 
        {
            if (_tween != null && _tween.IsPlaying()) // TODO: extensions
                _tween.Kill();
            
            _tween = DOTween.To(() => target.value, x => target.value = x, endValue, duration)
                .SetOptions(false)
                .SetTarget(target)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}