using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Services
{
    public interface IAssetProvider 
    {
        T LoadFromResources<T>(string path) where T : Object;
        
        UniTask InitializeAsync();
        
        UniTask<T> Load<T>(string key) where T : class;
        UniTask<T> LoadAndGetComponent<T>(string key) where T : MonoBehaviour;
        UniTask<T[]> LoadAll<T>(List<string> keys) where T : class;
        
        UniTask<List<string>> FetchAssetKeysByLabel<T>(string label);
        UniTask WarmupAssetsByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
    }
}