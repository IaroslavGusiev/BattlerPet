using System;
using VContainer;
using Code.Gameplay;
using VContainer.Unity;
using Code.Gameplay.Core;
using Code.Infrastructure;
using Code.Gameplay.Core.AI;
using Code.Gameplay.Battlefield;

namespace Code.LifetimeScopes
{
    public class BattleAreaScope : SceneScope<BattleAreaStateMachine, BattleAreaBootstrapper>
    {
        protected override void OnConfigure(IContainerBuilder builder)
        {
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