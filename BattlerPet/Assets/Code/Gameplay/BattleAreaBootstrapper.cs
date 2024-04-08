using VContainer;
using System.Threading;
using Code.Gameplay.States;
using Code.Infrastructure;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Gameplay
{
    public class BattleAreaBootstrapper : ScopeBootstrapper<BattleAreaStateMachine>
    {
        public BattleAreaBootstrapper(BattleAreaStateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory) { }

        protected override async UniTask StartLogic(CancellationToken token)
        { 
            StateMachine.RegisterState(StateFactory.Create<CreateBattlefieldState>(Lifetime.Scoped));
            await StateMachine.Enter<CreateBattlefieldState>();
        }
    }
}