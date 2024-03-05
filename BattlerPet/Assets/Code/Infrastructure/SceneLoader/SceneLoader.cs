using System;
using Code.Data;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        public UniTask Load(SceneName scene, Action onLoaded = null) =>
            LoadSceneAsync(scene.ToString(), LoadSceneMode.Single, onLoaded);

        public UniTask UnloadScene(SceneName scene, Action onUnloaded = null) =>
            UnloadSceneAsync(scene.ToString(), onUnloaded);

        public UniTask LoadInAdditiveMode(SceneName scene, Action onLoaded = null) =>
            LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive, onLoaded);

        public UniTask LoadInAdditiveModeWithScopeParenting<T>(SceneName scene, Action onLoaded = null) where T : LifetimeScope
        {
            LifetimeScope parentScope = LifetimeScope.Find<T>();
            return LoadSceneWithScopeParentingAsync(scene.ToString(), parentScope, onLoaded);
        }

        private async UniTask LoadSceneAsync(string nextScene, LoadSceneMode loadMode, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }
            
            await SceneManager
                .LoadSceneAsync(nextScene, loadMode)
                .ToUniTask();
            
            onLoaded?.Invoke();
        }

        private async UniTask UnloadSceneAsync(string sceneName, Action onUnloaded = null)
        {
            await SceneManager
                .UnloadSceneAsync(sceneName)
                .ToUniTask();
            
            onUnloaded?.Invoke();
        }

        private async UniTask LoadSceneWithScopeParentingAsync(string sceneName, LifetimeScope scope, Action onLoaded = null)
        {
            using (LifetimeScope.EnqueueParent(scope))
            {
                await SceneManager
                    .LoadSceneAsync(sceneName, LoadSceneMode.Additive)
                    .ToUniTask();
                
                onLoaded?.Invoke();
            }
        }
    }
}