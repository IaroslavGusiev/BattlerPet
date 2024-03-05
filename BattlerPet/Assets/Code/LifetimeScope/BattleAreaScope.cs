using System;
using VContainer;
using Code.Gameplay;
using VContainer.Unity;
using Code.Gameplay.Core;
using Code.Infrastructure;
using Code.Gameplay.Battlefield;
using Code.Gameplay.Core.AI;
using Code.Infrastructure.StateMachineBase;

namespace Code.LifetimeScopes
{
    public class BattleAreaScope : LifetimeScope 
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BattleAreaBootstrapper>();

            builder
                .Register<BattleAreaStateMachine>(Lifetime.Scoped)
                .AsSelf();
            
            builder.Register<StateFactory>(Lifetime.Scoped)
                .AsSelf();
            
            builder
                .Register<GameFactory>(Lifetime.Scoped)
                .AsImplementedInterfaces();
            
            builder
                .Register<Battlefield>(Lifetime.Scoped)
                .As<IBattlefield>();
            
            builder
                .Register<EntityRegister>(Lifetime.Scoped)
                .As<IEntityRegister>();
            
            builder
                .Register<BattleStarter>(Lifetime.Scoped)
                .AsSelf();

            builder.
                Register<EntityRandomizer>(Lifetime.Scoped) // TODO: parameter
                .AsSelf();

            builder
                .Register<DeathService>(Lifetime.Scoped)
                .As<IDeathService>();

            builder
                .Register<SkillCooldownService>(Lifetime.Scoped)
                .AsSelf();

            builder.Register<BattleTurnService>(Lifetime.Scoped)
                .As<IBattleTurnService, IAsyncStartable, IDisposable>();

            builder
                .Register<HasteService>(Lifetime.Scoped)
                .AsSelf();

            builder
                .Register<SkillSolver>(Lifetime.Scoped)
                .As<ISkillSolver>();

            builder
                .Register<StupidAI>(Lifetime.Scoped)
                .As<IArtificialIntelligence>();

            builder
                .Register<TargetChooser>(Lifetime.Scoped)
                .As<ITargetChooser>();
        }
    }
}