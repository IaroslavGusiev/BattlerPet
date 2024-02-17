using Code.Services;
using Cysharp.Threading.Tasks;
using Code.UI.ShowWindowsService;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class BootstrapState : IState
    {
        private readonly AdsService _adsService;
        private readonly IAssetProvider _assetProvider;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly ScreenService _screenService;

        public BootstrapState(
            GameStateMachine gameStateMachine, 
            ScreenService screenService, 
            AdsService adsService, 
            IStaticDataService staticDataService, 
            IAssetProvider assetProvider)
        {
            _adsService = adsService;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
            _screenService = screenService;
        }

        public async UniTask Enter()
        {
           await InitializeServices(); 
           await _gameStateMachine.Enter<LoadPlayerProgressState>();
        }

        public async UniTask Exit()
        { 
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Configs);
        }

        private async UniTask InitializeServices()
        {
            await _assetProvider.InitializeAsync();
            await _staticDataService.Initialize();
            _adsService.Initialize();
            _screenService.Initialize();
        }
    }
}