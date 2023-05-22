using System;
using Code.Services;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class StateFactory
    {
        #region BootstrapStateDependencies
        private readonly IAdsService _adsService;
        private readonly IStaticDataService _staticDataService;
        #endregion

        #region LoadPlayerProgressStateDependencies
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPlayerProgressService _playerProgress;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;
        #endregion
        
        public StateFactory(IAdsService adsService, IStaticDataService staticDataService, ISaveLoadService saveLoadService, IPlayerProgressService playerProgress, IEnumerable<IProgressReader> progressReaderServices)
        {
            _adsService = adsService;
            _staticDataService = staticDataService;
            
            _saveLoadService = saveLoadService;
            _playerProgress = playerProgress;
            _progressReaderServices = progressReaderServices;
        }

        public TState Create<TState>(IGameStateMachine gameStateMachine) where TState : IState
        {
            if (typeof(TState) == typeof(BootstrapState))
                return (TState)(IState)new BootstrapState(gameStateMachine, _adsService, _staticDataService);
            
            if (typeof(TState) == typeof(LoadPlayerProgressState))
                return (TState)(IState)new LoadPlayerProgressState(_saveLoadService, gameStateMachine, _playerProgress, _progressReaderServices);
            
            throw new ArgumentException($"Unsupported state type: {typeof(TState)}");
        }

        public BootstrapState Create(IGameStateMachine gameStateMachine) => 
            new(gameStateMachine, _adsService, _staticDataService);
    }
}