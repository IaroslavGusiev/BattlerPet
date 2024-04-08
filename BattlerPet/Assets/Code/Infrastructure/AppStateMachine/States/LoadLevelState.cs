using Code.Data;
using Code.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class LoadLevelState : IPaylodedState<SceneName>
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader)
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter(SceneName scene)
        {
            _loadingCurtain.Show();
            await _sceneLoader.Load(scene, () => _loadingCurtain.Hide());
        }

        public async UniTask Exit()
        {
            
        }
    }
}