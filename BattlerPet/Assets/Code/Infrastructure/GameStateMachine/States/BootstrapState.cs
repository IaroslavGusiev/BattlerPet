using UnityEngine;
using Code.Services;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly StaticDataService _staticDataService;
        private readonly IEnumerable<IInitializeHandler> _initializeHandlers;

        public BootstrapState(IGameStateMachine gameStateMachine, StaticDataService staticDataService, IEnumerable<IInitializeHandler> initializeHandlers) 
        {
            _initializeHandlers = initializeHandlers;
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }
        
        public void Enter()
        {
            Debug.Log("<color=green>Enter BootstrapState</color>");
            InitializeServices();
            _gameStateMachine.Enter<LoadPlayerProgressState>();
        }

        public void Exit()
        {
            
        }

        private void InitializeServices()
        {
            _staticDataService.LoadData();
            
            foreach (IInitializeHandler initializeHandler in _initializeHandlers)
                initializeHandler.Initialize();
        }
    }
}