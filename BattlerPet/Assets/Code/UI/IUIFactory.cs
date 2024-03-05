using Cysharp.Threading.Tasks;
using Code.UI.ScreenServiceSpace;
using UnityEngine.AddressableAssets;

namespace Code.UI
{
    public interface IUIFactory
    {
        UniTask CreateHUDRoot();
        UniTask<TScreen> CreateScreen<TScreen>(AssetReference assetReference) where TScreen : BaseScreen;
        UniTask<TScreen> CreateScreen<TScreen, TArg>(AssetReference assetReference, TArg arg) where TScreen : BaseScreen<TArg>;
    }
}