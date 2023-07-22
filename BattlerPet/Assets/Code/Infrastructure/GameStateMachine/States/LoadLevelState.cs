using Code.Data;
using Code.UI.LoadingCurtain;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadLevelState : IPaylodedState<SceneName>
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

        public void Enter(SceneName scene)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(scene, () => _loadingCurtain.Hide());
        }

        public void Exit()
        {
            
        }
    }
}