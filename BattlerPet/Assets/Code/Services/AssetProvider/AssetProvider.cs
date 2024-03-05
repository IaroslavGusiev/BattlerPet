using System;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, Object> _cachedObjects = new();
        private readonly Dictionary<string, AsyncOperationHandle> _assetsRequests = new();

        public async UniTask InitializeAsync() => 
            await Addressables.InitializeAsync().ToUniTask();
        
        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : MonoBehaviour => 
            await LoadAndGetComponent<TAsset>(assetReference.AssetGUID);

        public async UniTask<T> Load<T>(string key) where T : class
        {
            if (!_assetsRequests.TryGetValue(key, out AsyncOperationHandle handle))
            {
                handle = Addressables.LoadAssetAsync<T>(key);
                _assetsRequests.Add(key, handle);
            }
            await handle.ToUniTask();
            return handle.Result as T;
        }
        
        public async UniTask<T> LoadAndGetComponent<T>(string key) where T : MonoBehaviour
        {
            var prefab = await Load<GameObject>(key);
            if (prefab != null)
            {
                if (prefab.TryGetComponent(out T component))
                    return component;
                
                Debug.LogError($" Failed to get component of type { typeof(T) } from prefab { key } ");
            }
            return default;
        }

        public async UniTask<T[]> LoadAll<T>(List<string> keys) where T : class
        {
            var tasks = new List<UniTask<T>>(keys.Count);
            tasks.AddRange(Enumerable.Select(keys, Load<T>));
            return await UniTask.WhenAll(tasks);
        }

        public async UniTask<List<string>> FetchAssetKeysByLabel<T>(string label) =>
            await FetchAssetKeysByLabel(label, typeof(T));

        public async UniTask WarmupAssetsByLabel(string label) 
        {
            List<string> assetKeys = await FetchAssetKeysByLabel(label);
            await LoadAll<object>(assetKeys);
        }

        public async UniTask ReleaseAssetsByLabel(string label)
        {
            List<string> assetKeys = await FetchAssetKeysByLabel(label);
            foreach (string key in assetKeys)
            {
                if (_assetsRequests.TryGetValue(key, out AsyncOperationHandle handler))
                {
                    Addressables.Release(handler);
                    _assetsRequests.Remove(key);
                }
            }
        }
        
        public T LoadFromResources<T>(string path) where T : Object // TODO: maybe own class for it
        {
            if (_cachedObjects.TryGetValue(path, out Object prefab))
                return prefab as T;

            Object loadedPrefab = Resources.Load<T>(path);
            _cachedObjects[path] = loadedPrefab;
            return (T) loadedPrefab;
        }

        private async UniTask<List<string>> FetchAssetKeysByLabel(string label, Type type = null)
        { 
            AsyncOperationHandle<IList<IResourceLocation>> operationHandle = Addressables.LoadResourceLocationsAsync(label, type);
            IList<IResourceLocation> locations = await operationHandle.ToUniTask();
            
            var assetKeys = new List<string>(locations.Count);
            assetKeys.AddRange(locations.Select(location => location.PrimaryKey));
            
            Addressables.Release(operationHandle);
            return assetKeys;
        }
    }
}