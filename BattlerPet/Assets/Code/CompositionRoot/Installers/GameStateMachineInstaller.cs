using VContainer;
using VContainer.Unity;
using Code.Infrastructure.GameStateMachine;

namespace Code.CompositionRoot
{
    public class GameStateMachineInstaller : IInstaller
    {
        private IContainerBuilder _builder;
        
        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterGameStateMachine();
            RegisterStateFactories();
        }

        private void RegisterGameStateMachine()
        {
            _builder.Register<GameStateMachine>(Lifetime.Singleton)
                .As<IGameStateMachine>();
        }

        private void RegisterStateFactories()
        {
            RegisterBootstrapStateFactory();
            RegisterLoadPlayerProgressStateFactory();
            RegisterLoadLevelStateFactory();
        }
        
        private void RegisterBootstrapStateFactory()
        {
            _builder.Register<BootstrapStateFactory>(Lifetime.Singleton);
            _builder.RegisterFactory<IGameStateMachine, BootstrapState>(container =>
                container.Resolve<BootstrapStateFactory>().Create, Lifetime.Singleton);
        }

        private void RegisterLoadPlayerProgressStateFactory()
        {
            _builder.Register<LoadPlayerProgressStateFactory>(Lifetime.Singleton);
            _builder.RegisterFactory<IGameStateMachine, LoadPlayerProgressState>(container =>
                container.Resolve<LoadPlayerProgressStateFactory>().Create, Lifetime.Singleton);
        }

        private void RegisterLoadLevelStateFactory()
        {
            _builder.Register<LoadLevelStateFactory>(Lifetime.Singleton);
            _builder.RegisterFactory<IGameStateMachine, LoadLevelState>(container =>
                container.Resolve<LoadLevelStateFactory>().Create, Lifetime.Singleton);
        }
    }
}