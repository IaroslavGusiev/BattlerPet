using UnityEngine;
using Code.Infrastructure.Services;

namespace Code.Infrastructure.GameStateMachine
{
    public class BootstrapState : IState
    {
        private readonly IAdsService _adsService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IGameStateMachine gameStateMachine, IAdsService adsService, IStaticDataService staticDataService) 
        {
            _adsService = adsService;
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }
        
        public void Enter()
        {
            InitializeServices();
            Debug.Log("<color=green>Enter BootstrapState</color>");
        }

        public void Exit()
        {
            
        }

        private void InitializeServices()
        {
            _adsService.Initialize();
            _staticDataService.Initialize();
        }
    }
}