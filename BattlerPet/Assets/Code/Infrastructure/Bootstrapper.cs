using System;
using VContainer;
using UnityEngine;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, IInitializable, IDisposable
    {
        private ISceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public void Initialize()
        {
            Debug.Log("<color=yellow>Bootstrapper Initialize</color>");
            _sceneLoader.Load("Game");
        }

        public void Dispose()
        {
            Debug.Log("<color=red>Bootstrapper Dispose</color>");
        }
    }
}
