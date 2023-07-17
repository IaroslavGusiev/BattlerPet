using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, Object> _cachedObjects = new Dictionary<string, Object>();
        private readonly Dictionary<string, List<Object>> _cachedListOfObjects = new Dictionary<string, List<Object>>();

        public T Get<T>(string path) where T : Object
        {
            if (_cachedObjects.TryGetValue(path, out Object prefab))
                return prefab as T;

            Object loadedPrefab = Resources.Load<T>(path);
            _cachedObjects[path] = loadedPrefab;
            return (T) loadedPrefab;
        }

        public List<T> GetAll<T>(string path) where T : Object
        {
            if (_cachedListOfObjects.TryGetValue(path, out List<Object> prefabList))
            {
                List<T> castList = prefabList.Cast<T>().ToList();
                return castList;
            }
            
            List<T> loadedObjects = Resources.LoadAll<T>(path).ToList();
            _cachedListOfObjects[path] = new List<Object>(loadedObjects);
            return loadedObjects;
        }
    }
}