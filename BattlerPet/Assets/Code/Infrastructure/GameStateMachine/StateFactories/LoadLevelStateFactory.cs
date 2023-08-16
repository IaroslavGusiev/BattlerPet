using System;
using Code.UI.LoadingCurtain;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadLevelStateFactory : IStateFactory
    {
        public Type StateType => typeof(LoadLevelState);
        
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;
        
        public LoadLevelStateFactory(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public IExitableState Create(IGameStateMachine gameStateMachine) =>
            new LoadLevelState(gameStateMachine, _loadingCurtain, _sceneLoader);
    }
}