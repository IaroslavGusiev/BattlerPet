using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Code.Services
{
    public interface IAssetProvider 
    {
        UniTask InitializeAsync();
        
        UniTask<T> Load<T>(string key) where T : class;
        UniTask<T> LoadAndGetComponent<T>(string key) where T : MonoBehaviour;
        UniTask<T[]> LoadAll<T>(List<string> keys) where T : class;
        
        UniTask<List<string>> FetchAssetKeysByLabel<T>(string label);
        UniTask WarmupAssetsByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : MonoBehaviour;
        T LoadFromResources<T>(string path) where T : Object;
    }
}