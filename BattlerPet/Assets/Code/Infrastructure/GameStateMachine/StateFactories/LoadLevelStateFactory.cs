using Code.UI.LoadingCurtain;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadLevelStateFactory
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelStateFactory(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public LoadLevelState Create(IGameStateMachine gameStateMachine) => 
            new(gameStateMachine, _loadingCurtain, _sceneLoader);
    }
}