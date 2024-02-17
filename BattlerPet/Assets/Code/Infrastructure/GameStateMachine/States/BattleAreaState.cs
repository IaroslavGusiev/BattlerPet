using Code.Data;
using Code.Services;
using Code.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class BattleAreaState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly ILoadingCurtain _loadingCurtain;

        public BattleAreaState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter()
        {
            _loadingCurtain.Show();
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Entities);
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Battlefield);
            await _sceneLoader.Load(SceneName.BattleArea, () => _loadingCurtain.Hide());
        }

        public async UniTask Exit()
        {
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Entities);
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Battlefield);
        }
    }
}