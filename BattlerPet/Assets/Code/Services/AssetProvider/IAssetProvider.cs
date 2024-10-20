using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Code.Services
{
    public interface IAssetProvider 
    {
        UniTask InitializeAsync(CancellationToken token);
        
        UniTask<T> Load<T>(string key) where T : class;
        UniTask<T> LoadAndGetComponent<T>(string key) where T : MonoBehaviour;
        UniTask<T[]> LoadAll<T>(List<string> keys) where T : class;
        
        UniTask<List<string>> FetchAssetKeysByLabel<T>(string label, CancellationToken token);
        UniTask WarmupAssetsByLabel(string label, CancellationToken token);
        UniTask ReleaseAssetsByLabel(string label, CancellationToken token);
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : MonoBehaviour;
        T LoadFromResources<T>(string path) where T : Object;
    }
}