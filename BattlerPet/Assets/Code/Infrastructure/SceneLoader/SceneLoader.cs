using System;
using Code.Data;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(SceneName scene, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadSceneAsync(scene.ToString(), LoadSceneMode.Single, onLoaded));

        public void LoadInAdditiveMode(SceneName scene, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive, onLoaded));

        public void UnloadScene(SceneName scene, Action onUnloaded = null) =>
            _coroutineRunner.StartCoroutine(UnloadSceneAsync(scene.ToString(), onUnloaded));

        private IEnumerator LoadSceneAsync(string nextScene, LoadSceneMode loadMode, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
      
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene, loadMode);

            while (!waitNextScene.isDone)
                yield return null;
      
            onLoaded?.Invoke();
        }

        private IEnumerator UnloadSceneAsync(string sceneName, Action onUnloaded = null)
        {
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneName);
            
            while (!unloadScene.isDone)
                yield return null;
            
            onUnloaded?.Invoke();
        }
    }
}