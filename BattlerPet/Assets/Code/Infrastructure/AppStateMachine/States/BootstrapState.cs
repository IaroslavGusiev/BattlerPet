﻿using Code.Services;
using Cysharp.Threading.Tasks;
using Code.UI.ScreenServiceSpace;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class BootstrapState : IState
    {
        private readonly AdsService _adsService;
        private readonly IAssetProvider _assetProvider;
        private readonly AppStateMachine _appStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly ScreenService _screenService;

        public BootstrapState(
            AppStateMachine appStateMachine, 
            ScreenService screenService, 
            AdsService adsService, 
            IStaticDataService staticDataService, 
            IAssetProvider assetProvider)
        {
            _adsService = adsService;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _appStateMachine = appStateMachine;
            _screenService = screenService;
        }

        public async UniTask Enter()
        {
           await InitializeServices(); 
           await _appStateMachine.Enter<LoadPlayerProgressState>();
        }

        public async UniTask Exit() => 
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Configs);

        private async UniTask InitializeServices()
        {
            await _assetProvider.InitializeAsync();
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Configs);
            await _staticDataService.Initialize();
            _adsService.Initialize();
            _screenService.Initialize();
        }
    }
}