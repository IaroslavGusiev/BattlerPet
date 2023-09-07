using System;
using Code.Data;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(SceneName scene, Action onLoaded = null) =>
            LoadSceneAsync(scene.ToString(), LoadSceneMode.Single, onLoaded).Forget();

        public void LoadInAdditiveMode(SceneName scene, Action onLoaded = null) =>
            LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive, onLoaded).Forget();

        public void UnloadScene(SceneName scene, Action onUnloaded = null) =>
            UnloadSceneAsync(scene.ToString(), onUnloaded).Forget();

        private async UniTaskVoid LoadSceneAsync(string nextScene, LoadSceneMode loadMode, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }
            
            var loadScene = SceneManager
                .LoadSceneAsync(nextScene, loadMode)
                .ToUniTask();
            
            await loadScene;
            onLoaded?.Invoke();
        }

        private async UniTaskVoid UnloadSceneAsync(string sceneName, Action onUnloaded = null)
        {
            var unloadScene = SceneManager
                .UnloadSceneAsync(sceneName)
                .ToUniTask();
            
            await unloadScene;
            onUnloaded?.Invoke();
        }
    }
}