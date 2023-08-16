using System;
using Code.Services;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadPlayerProgressStateFactory : IStateFactory
    {
        public Type StateType => typeof(LoadPlayerProgressState);
        
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPlayerProgressProvider _playerProgress;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;
        
        public LoadPlayerProgressStateFactory(ISaveLoadService saveLoadService, IPlayerProgressProvider playerProgress, IEnumerable<IProgressReader> progressReaderServices)
        {
            _playerProgress = playerProgress;
            _saveLoadService = saveLoadService;
            _progressReaderServices = progressReaderServices;
        }

        public IExitableState Create(IGameStateMachine gameStateMachine) => 
            new LoadPlayerProgressState(gameStateMachine,_saveLoadService, _playerProgress, _progressReaderServices);
    }
}