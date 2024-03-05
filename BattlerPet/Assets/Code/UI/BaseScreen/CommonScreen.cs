using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.UI.ScreenServiceSpace
{
    public abstract class CommonScreen : MonoBehaviour
    {
        [field: SerializeField] public AssetReference AssetReference { get; private set; }
        private bool _isInAnimation;
        
        public async UniTask ShowAsync()
        {
            if (_isInAnimation)
            {
                Debug.LogError($"Can't play <Show> animation of <{name}> while another animation is playing");
                return;
            }
            _isInAnimation = true;
            await PlayShowAnimationAsync();
            _isInAnimation = false;
        }

        public async UniTask HideAsync()
        {
            if (_isInAnimation)
            {
                Debug.LogError($"Can't play <Hide> animation of <{name}> while another animation is playing");
                return;
            }
            _isInAnimation = true;
            await PlayHideAnimationAsync();
            _isInAnimation = false;
        }
        
        protected virtual async UniTask PlayShowAnimationAsync()
        {
            await UniTask.CompletedTask;
        }
        
        protected virtual async UniTask PlayHideAnimationAsync()
        {
            await UniTask.CompletedTask;
        }

    }
}