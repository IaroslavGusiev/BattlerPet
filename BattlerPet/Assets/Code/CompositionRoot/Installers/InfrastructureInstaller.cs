using VContainer;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;
using Code.Infrastructure.StateMachineBase;
using Code.Infrastructure.GameStateMachineScope;

namespace Code.CompositionRoot
{
    public class InfrastructureInstaller : IInstaller
    {
        private IContainerBuilder _builder;
        
        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterGameStateMachine();
            RegisterStateFactory();
            RegisterSceneLoader();
            RegisterAssetProvider();
        }
        
        private void RegisterGameStateMachine()
        {
            _builder
                .Register<GameStateMachine>(Lifetime.Singleton)
                .AsSelf();
        }

        private void RegisterStateFactory()
        {
            _builder
                .Register<StateFactory>(Lifetime.Singleton)
                .AsSelf();
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