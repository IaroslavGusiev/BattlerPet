﻿using VContainer;

namespace Code.Infrastructure.GameStateMachine
{
    public class StateFactoryRegister
    {
        private readonly IContainerBuilder _builder;
        
        public StateFactoryRegister(IContainerBuilder builder) => 
            _builder = builder;

        public void RegisterStateFactories()
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