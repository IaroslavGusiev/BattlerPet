using VContainer;
using Code.Services;

namespace Code.Infrastructure.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private IObjectResolver _objectResolver;
        private IAssetProvider _assetProvider;

        public GameFactory(IObjectResolver objectResolver, IAssetProvider assetProvider)
        {
            _objectResolver = objectResolver;
            _assetProvider = assetProvider;
        }
    }
}