using System;

namespace CodeBase.Infrastructure.Observables
{
    /// <summary>
    /// Wraps a value in order to allow observing its value change
    /// </summary>
    /// <example>>
    ///   var obs = new Observable<int>(123);
    ///   obs.OnChanged += (o, oldVal, newVal) => Log("changed from " + oldVal + " to " + newVal);
    ///   obs.Value = 456; // dispatches OnChanged
    /// </example>
    /// <author>Jackson Dunstan, http://JacksonDunstan.com/articles/3547</author>
    /// <license>MIT</license>
    [Serializable]
    public class Observable<T> : IEquatable<Observable<T>>
    {
        public Action<Observable<T>, T, T> OnChanged;
        
        private T _value;

        public Observable() { }
        
        public Observable(T value) => 
            _value = value;

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;
                _value = value;
                OnChanged?.Invoke(this, oldValue, value);
            }
        }
 
        public static implicit operator Observable<T>(T observable)
        {
            return new Observable<T>(observable);
        }
 
        public static explicit operator T(Observable<T> observable)
        {
            return observable._value;
        }
 
        public override string ToString() => 
            _value.ToString();

        public bool Equals(Observable<T> other) =>
            other._value.Equals(_value);

        public override bool Equals(object other) =>
         other is Observable<T> observable && observable._value.Equals(_value);

        public override int GetHashCode() => 
            _value.GetHashCode();
    }
}