using System;
using VContainer;
using UnityEngine;
using VContainer.Unity;
using Code.Infrastructure.GameStateMachine;

namespace Code.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, IInitializable, IDisposable
    {
        private ISceneLoader _sceneLoader;
        private IGameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }

        public void Dispose()
        {
            Debug.Log("<color=red>Bootstrapper Dispose</color>");
        }
    }
}
