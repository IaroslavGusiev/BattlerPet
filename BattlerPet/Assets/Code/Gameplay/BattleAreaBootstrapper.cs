using VContainer;
using System.Threading;
using VContainer.Unity;
using Code.Gameplay.States;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Gameplay
{
    public class BattleAreaBootstrapper : IAsyncStartable
    {
        private readonly BattleAreaStateMachine _stateMachine;
        private readonly StateFactory _stateFactory;

        public BattleAreaBootstrapper(BattleAreaStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            _stateMachine.RegisterState(_stateFactory.Create<CreateBattlefieldState>(Lifetime.Scoped));
            await _stateMachine.Enter<CreateBattlefieldState>();
        }
    }
}