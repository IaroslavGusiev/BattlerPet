using System;
using System.Linq;
using System.Collections.Generic;
using VContainer;

namespace Code.Infrastructure.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _registeredStates = new Dictionary<Type, IExitableState>();
        private IExitableState _currentState;

        private readonly BootstrapStateFactory _factory;

        public GameStateMachine(BootstrapStateFactory bootstrapStateFactory) // IEnumerable<IExitableState> states
        {
            _factory = bootstrapStateFactory;
            _registeredStates.Add(typeof(BootstrapState), _factory.Create(this));
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
            _registeredStates[typeof(TState)] as TState;
    }
}