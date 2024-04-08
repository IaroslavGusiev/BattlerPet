using VContainer;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;

namespace Code.CompositionRoot
{
    public class InfrastructureInstaller : IInstaller
    {
        private IContainerBuilder _builder;
        
        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterSceneLoader();
            RegisterAssetProvider();
        }
        
        private void RegisterSceneLoader()
        {
            _builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>()
                .AsSelf();
        }

        private void RegisterAssetProvider()
        {
            _builder.Register<AssetProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
        }
    }
}