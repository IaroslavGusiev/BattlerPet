using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Infrastructure.StateMachineBase
{
    public abstract class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _activeState;

        public async UniTask Enter<TState>() where TState : class, IState
        {
            var newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            var newState = await ChangeState<TState>();
            await newState.Enter(payload);
        }
        
        public void RegisterState<TState>(TState state) where TState : IExitableState =>
            _states.Add(typeof(TState), state);

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (_activeState != null)
                await _activeState.Exit();

            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}