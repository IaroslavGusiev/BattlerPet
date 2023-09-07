using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner, IDisposable
    {
        private readonly List<IEnumerator> _currentlyRunningCoroutines = new();

        public void RunCoroutine(IEnumerator coroutine)
        {
            _currentlyRunningCoroutines.Add(coroutine);
            StartCoroutine(WrapCoroutine(coroutine));
        }

        public void StopRequiredCoroutine(IEnumerator coroutine)
        {
            if (_currentlyRunningCoroutines.Contains(coroutine))
                _currentlyRunningCoroutines.Remove(coroutine);
            
            StopCoroutine(coroutine);
        }

        public void Dispose()
        {
            foreach (IEnumerator coroutine in _currentlyRunningCoroutines)
                StopCoroutine(coroutine);
        }

        private IEnumerator WrapCoroutine(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine); 
            _currentlyRunningCoroutines.Remove(coroutine);
        }
    }
}