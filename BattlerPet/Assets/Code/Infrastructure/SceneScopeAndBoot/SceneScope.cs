using VContainer;
using VContainer.Unity;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public abstract class SceneScope<TStateMachine, TBootstrapper> : LifetimeScope 
        where TStateMachine : StateMachine 
        where TBootstrapper : ScopeBootstrapper<TStateMachine>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TBootstrapper>();
            
            builder
                .Register<TStateMachine>(Lifetime.Scoped)
                .AsSelf();

            builder
                .Register<StateFactory>(Lifetime.Scoped)
                .AsSelf();
            
            OnConfigure(builder);
        }
        
        protected abstract void OnConfigure(IContainerBuilder builder);
    }
}