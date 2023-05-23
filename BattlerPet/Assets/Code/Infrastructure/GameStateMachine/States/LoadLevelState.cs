using Code.UI.LoadingCurtain;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IGameStateMachine gameStateMachine, ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, () => _loadingCurtain.Hide());
        }

        public void Exit()
        {
            
        }
    }
}