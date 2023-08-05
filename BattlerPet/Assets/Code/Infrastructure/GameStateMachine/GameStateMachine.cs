﻿using System;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        
        public GameStateMachine(BootstrapStateFactory bootstrapStateFactory, LoadPlayerProgressStateFactory loadPlayerProgressStateFactory, LoadLevelStateFactory loadLevelStateFactory)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = bootstrapStateFactory.Create(this),
                [typeof(LoadPlayerProgressState)] = loadPlayerProgressStateFactory.Create(this),
                [typeof(LoadLevelState)] = loadLevelStateFactory.Create(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            var newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            var state = GetState<TState>();
            _currentState = state;
            return state;
        }
    
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}