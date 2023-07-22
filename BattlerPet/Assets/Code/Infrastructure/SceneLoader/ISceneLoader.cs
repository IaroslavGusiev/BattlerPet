using System;
using Code.Data;

namespace Code.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(SceneName scene, Action onLoaded = null);
        void LoadInAdditiveMode(SceneName scene, Action onLoaded = null);
        void UnloadScene(SceneName scene, Action onUnloaded = null);
    }
}