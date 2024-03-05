using System;
using DG.Tweening;
using UnityEngine;
using VContainer.Unity;

namespace Code.Infrastructure.UpdateRunner
{
    public class DoTweenUpdater : ITickListener, IInitializable, IDisposable
    {
        private ITickSource _tickSource;

        public DoTweenUpdater(ITickSource tickSource) // TODO: rename something like DoTween Core
        {
            _tickSource = tickSource;
            _tickSource.AddListener(this);
        }

        public void Initialize()
        {
            DOTween.useSmoothDeltaTime = false;
            DOTween.SetTweensCapacity(50, 10);
        }

        public void Tick(float deltaTime)
        {
            // TODO: logic for pause
            DOTween.ManualUpdate(deltaTime, Time.unscaledDeltaTime);
        }

        public void Dispose()
        {
            _tickSource.RemoveListener(this);
            _tickSource = null;
        }
    }
}