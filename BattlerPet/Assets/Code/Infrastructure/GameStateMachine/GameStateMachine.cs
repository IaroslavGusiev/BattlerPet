using System;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _currentState;
        
        public GameStateMachine(BootstrapStateFactory bootstrapStateFactory, LoadPlayerProgressStateFactory loadPlayerProgressStateFactory) 
        {
            _states.Add(typeof(BootstrapState), bootstrapStateFactory.Create(this));
            _states.Add(typeof(LoadPlayerProgressState), loadPlayerProgressStateFactory.Create(this));
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
      
            TState state = GetState<TState>();
            _currentState = state;
      
            return state;
        }
    
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}