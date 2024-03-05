using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Code.Data
{
    public abstract class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
    {
        public readonly Dictionary<TKey, TValue> Dictionary = new();

        [SerializeField] private TKey[] _keys;
        [SerializeField] private TValue[] _values;

        public void OnBeforeSerialize()
        {
            _keys = Dictionary.Keys.ToArray();
            _values = Dictionary.Values.ToArray();
        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < _keys.Length; i++)
                Dictionary.Add(_keys[i], _values[i]);
        }
    }
}