using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Observables
{
    [Serializable]
    public class Publisher<TSubject> : IPublisher<TSubject>
    {
        public event Action<TSubject> OnChange;
        
        [SerializeField] private TSubject _subject;
        
        public TSubject Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnChange?.Invoke(_subject);
            }
        }
    }
}