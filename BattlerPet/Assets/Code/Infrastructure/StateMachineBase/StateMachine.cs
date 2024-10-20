using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Infrastructure.StateMachineBase
{
    public abstract class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _activeState;

        public async UniTask Enter<TState>(CancellationToken token) where TState : class, IState
        {
            var newState = await ChangeState<TState>(token);
            await newState.Enter(token);
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload, CancellationToken token) where TState : class, IPaylodedState<TPayload>
        {
            var newState = await ChangeState<TState>(token);
            await newState.Enter(payload, token);
        }
        
        public void RegisterState<TState>(TState state) where TState : IExitableState =>
            _states.Add(typeof(TState), state);

        private async UniTask<TState> ChangeState<TState>(CancellationToken token) where TState : class, IExitableState
        {
            if (_activeState != null)
                await _activeState.Exit(token);

            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}