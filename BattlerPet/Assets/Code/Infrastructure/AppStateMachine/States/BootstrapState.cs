using Code.Services;
using System.Threading;
using Cysharp.Threading.Tasks;
using Code.UI.ScreenServiceSpace;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class BootstrapState : IState
    {
        private readonly ScreenService _screenService;
        private readonly IAssetProvider _assetProvider;
        private readonly AppStateMachine _appStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(AppStateMachine appStateMachine, ScreenService screenService, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _screenService = screenService;
            _assetProvider = assetProvider;
            _appStateMachine = appStateMachine;
            _staticDataService = staticDataService;
        }

        public async UniTask Enter(CancellationToken token)
        {
            await InitializeServices(token);
            await _appStateMachine.Enter<LoadPlayerProgressState>(token);
        }

        public async UniTask Exit(CancellationToken token) =>
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Configs, token);

        private async UniTask InitializeServices(CancellationToken token)
        {
            await _assetProvider.InitializeAsync(token);
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Configs, token);
            await _staticDataService.Initialize(token);
            _screenService.Initialize();
        }
    }
}