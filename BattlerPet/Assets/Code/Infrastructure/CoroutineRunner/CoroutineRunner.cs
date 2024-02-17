using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner, IDisposable
    {
        private readonly HashSet<IEnumerator> _currentlyRunningCoroutines = new();

        public void RunCoroutine(IEnumerator coroutine)
        {
            _currentlyRunningCoroutines.Add(coroutine);
            StartCoroutine(WrapCoroutine(coroutine));
        }

        public void StopRunningCoroutine(IEnumerator coroutine)
        {
            if (_currentlyRunningCoroutines.Contains(coroutine))
            {
                _currentlyRunningCoroutines.Remove(coroutine);
                StopCoroutine(coroutine);
            }
        }

        public void Dispose()
        {
            foreach (IEnumerator coroutine in _currentlyRunningCoroutines.Where(coroutine => coroutine != null))
                StopCoroutine(coroutine);

            _currentlyRunningCoroutines.Clear();
        }

        private IEnumerator WrapCoroutine(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine);
            _currentlyRunningCoroutines.Remove(coroutine);
        }
    }
}