using System;
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
            RegisterStateFactories();
            RegisterGameStateMachine();
        }

        private void RegisterGameStateMachine()
        {
            _builder
                .Register<GameStateMachine>(Lifetime.Singleton)
                .As<IGameStateMachine>();
        }

        private void RegisterStateFactories()
        {
            RegisterStateFactory<BootstrapStateFactory>(typeof(BootstrapState));
            RegisterStateFactory<LoadLevelStateFactory>(typeof(LoadLevelState));
            RegisterStateFactory<LoadPlayerProgressStateFactory>(typeof(LoadPlayerProgressState));
        }

        private void RegisterStateFactory<TFactory>(Type stateType) where TFactory : class, IStateFactory
        {
            _builder
                .Register<TFactory>(Lifetime.Singleton)
                .As<IStateFactory>()
                .WithParameter(stateType);
        }
    }
}