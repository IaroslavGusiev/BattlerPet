using Code.Services;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadPlayerProgressStateFactory
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPlayerProgressProvider _playerProgress;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;

        public LoadPlayerProgressStateFactory(ISaveLoadService saveLoadService, IPlayerProgressProvider playerProgress, IEnumerable<IProgressReader> progressReaderServices)
        {
            _playerProgress = playerProgress;
            _saveLoadService = saveLoadService;
            _progressReaderServices = progressReaderServices;
        }

        public LoadPlayerProgressState Create(IGameStateMachine gameStateMachine) => 
            new(gameStateMachine,_saveLoadService, _playerProgress, _progressReaderServices);
    }
}