using VContainer;
using System.Threading;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;
using Code.Infrastructure.GameStateMachineScope;

namespace Code.Infrastructure
{
    public class AppBootstrapper : ScopeBootstrapper<AppStateMachine>
    {
        public AppBootstrapper(AppStateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory) { }

        protected override async UniTask StartLogic(CancellationToken token)
        {
            StateMachine.RegisterState(StateFactory.Create<BootstrapState>(Lifetime.Scoped));
            StateMachine.RegisterState(StateFactory.Create<LoadPlayerProgressState>(Lifetime.Scoped));
            StateMachine.RegisterState(StateFactory.Create<BattleAreaState>(Lifetime.Scoped));
            await StateMachine.Enter<BootstrapState>();
        }
    }
}
