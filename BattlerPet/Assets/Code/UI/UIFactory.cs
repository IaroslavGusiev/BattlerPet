using Code.Data;
using Code.Services;
using Cysharp.Threading.Tasks;
using Code.UI.ScreenServiceSpace;
using Object = UnityEngine.Object;
using UnityEngine.AddressableAssets;

namespace Code.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private HUDRoot _root;

        public UIFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask CreateHUDRoot()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<HUDRoot>(CoreAssetPaths.HUDRootAssetPath);
            _root = Object.Instantiate(prefab);
            _root.SetMainCameraAsWorldCamera();
        }

        public async UniTask<TScreen> CreateScreen<TScreen>(AssetReference assetReference) where TScreen : BaseScreen
        {
            var prefab = await _assetProvider.Load<TScreen>(assetReference);
            TScreen screenInstance = Object.Instantiate(prefab, _root.transform);
            screenInstance.SetupOnInstantiate();
            return screenInstance;
        }

        public async UniTask<TScreen> CreateScreen<TScreen, TArg>(AssetReference assetReference, TArg arg) where TScreen : BaseScreen<TArg>
        {
            var prefab = await _assetProvider.Load<TScreen>(assetReference);
            TScreen screenInstance = Object.Instantiate(prefab, _root.transform);
            screenInstance.SetupOnInstantiate(arg);
            return screenInstance;
        }
    }
}