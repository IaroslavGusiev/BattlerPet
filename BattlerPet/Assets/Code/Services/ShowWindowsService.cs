using Code.Data;
using Code.Infrastructure;

namespace Code.Services
{
    public class ShowWindowsService
    {
        private ISceneLoader _sceneLoader;
        private IAssetProvider _assetProvider;

        public ShowWindowsService(ISceneLoader sceneLoader, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            
        }

        public void ShowWindow(WindowType windowType)
        {
            
        }
    }
}