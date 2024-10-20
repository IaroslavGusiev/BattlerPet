using Code.UI;
using Code.Data;
using Code.Services;
using System.Threading;
using Code.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class BattleAreaState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private readonly ILoadingCurtain _loadingCurtain;

        public BattleAreaState(ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, IAssetProvider assetProvider, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter(CancellationToken token)
        {
            _loadingCurtain.Show();
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Entities, token);
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Battlefield, token);
            await _sceneLoader.Load(SceneName.BattleArea, OnLoadedScene); 
        }

        public async UniTask Exit(CancellationToken token)
        {
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Entities, token);
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Battlefield, token);
        }

        private async void OnLoadedScene()
        {
            await _uiFactory.CreateHUDRoot();
            _loadingCurtain.Hide();
        }
    }
}