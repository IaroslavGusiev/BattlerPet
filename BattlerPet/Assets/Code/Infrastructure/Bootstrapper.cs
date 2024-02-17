using System;
using VContainer;
using UnityEngine;
using VContainer.Unity;
using System.Threading;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;
using Code.Infrastructure.GameStateMachineScope;

namespace Code.Infrastructure
{
    public class Bootstrapper : IAsyncStartable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly StateFactory _stateFactory;

        public Bootstrapper(GameStateMachine gameStateMachine, StateFactory stateFactory)
        {
            _gameStateMachine = gameStateMachine;
            _stateFactory = stateFactory;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            _gameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>(Lifetime.Scoped));
            _gameStateMachine.RegisterState(_stateFactory.Create<LoadPlayerProgressState>(Lifetime.Scoped));
            _gameStateMachine.RegisterState(_stateFactory.Create<BattleAreaState>(Lifetime.Scoped));
            await _gameStateMachine.Enter<BootstrapState>();
        }
        
        public void Dispose() => 
            Debug.Log("<color=red>Bootstrapper Dispose</color>");
    }
}
