using UnityEngine;

namespace Code.Infrastructure
{
    public class MonoBehaviourInstantiator
    {
        public T Create<T>(T prefab) where T : Object => 
            Object.Instantiate(prefab);
        
        public T Create<T>(T prefab, Vector3 at) where T : Object => 
            Object.Instantiate(prefab, at, Quaternion.identity);
        
        public T Create<T>(T prefab, Transform under) where T : Object => 
            Object.Instantiate(prefab, under);

        public T Create<T>(T prefab, Vector3 at, Quaternion rotation, Transform under) where T : Object=> 
            Object.Instantiate(prefab, at, rotation, under);
    }
}