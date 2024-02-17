using System;
using Code.Data;
using VContainer.Unity;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure
{
    public interface ISceneLoader
    {
        UniTask Load(SceneName scene, Action onLoaded = null);
        UniTask UnloadScene(SceneName scene, Action onUnloaded = null);
        UniTask LoadInAdditiveMode(SceneName scene, Action onLoaded = null);
        UniTask LoadInAdditiveModeWithScopeParenting<T>(SceneName scene, Action onLoaded = null) where T : LifetimeScope;
    }
}