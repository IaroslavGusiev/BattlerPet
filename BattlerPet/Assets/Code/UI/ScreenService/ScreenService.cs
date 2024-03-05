using Code.Services;
using Code.StaticData.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.UI.ScreenServiceSpace
{
    public class ScreenService : IScreenService 
    {
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;
        private ScreenServiceConfig _config;

        public ScreenService(IUIFactory uiFactory, IStaticDataService staticDataService)
        {
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void Initialize() => 
            _config = _staticDataService.GetScreenServiceConfig();

        public async UniTask<TScreen> ShowScreen<TScreen>() where TScreen : BaseScreen
        {
            AssetReference assetReference = _config.ScreenPrefabMap.GetPrefabReference<TScreen>();
            var screenInstance = await SpawnScreenFromPrefab<TScreen>(assetReference);
            return await PlayShowScreenAnim(screenInstance);
        }
        
        public async UniTask<TScreen> ShowScreen<TScreen, TArg>(TArg arg) where TScreen : BaseScreen<TArg>
        {
            AssetReference assetReference = _config.ScreenPrefabMap.GetPrefabReference<TScreen>();
            TScreen screenInstance = await SpawnScreenFromPrefab<TScreen, TArg>(arg, assetReference);
            return await PlayShowScreenAnim(screenInstance);
        }

        private async UniTask<TScreen> PlayShowScreenAnim<TScreen>(TScreen screenInstance) where TScreen : CommonScreen
        {
            await PlayShowScreenAsync(screenInstance);
            return screenInstance;
        }

        private async UniTask<TScreen> SpawnScreenFromPrefab<TScreen>(AssetReference assetReference) where TScreen : BaseScreen => 
            await _uiFactory.CreateScreen<TScreen>(assetReference);

        private async UniTask<TScreen> SpawnScreenFromPrefab<TScreen, TArg>(TArg arg, AssetReference assetReference) where TScreen : BaseScreen<TArg> => 
            await _uiFactory.CreateScreen<TScreen, TArg>(assetReference, arg);

        private async UniTask PlayShowScreenAsync(CommonScreen screen) =>
            await screen.ShowAsync();
    }
}