using Code.Data;
using UnityEngine;
using Code.Services;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadPlayerProgressState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayerProgressService _playerProgress;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;

        public LoadPlayerProgressState( IGameStateMachine gameStateMachine, ISaveLoadService saveLoadService, IPlayerProgressService playerProgress, IEnumerable<IProgressReader> progressReaderServices)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _playerProgress = playerProgress;
            _progressReaderServices = progressReaderServices;
        }

        public void Enter()
        {
            Debug.Log("LoadPlayerProgressState enter");
            PlayerProgress progress = LoadProgressOrInitNew();
            NotifyProgressReaderServices(progress);
            // _gameStateMachine.Enter<LoadLevelState, string>("Game");
        }

        public void Exit()
        {
            
        }

        private void NotifyProgressReaderServices(PlayerProgress progress)
        {
            foreach (IProgressReader reader in _progressReaderServices)
                reader.LoadProgress(progress);
        }

        private PlayerProgress LoadProgressOrInitNew()
        {
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
            return _playerProgress.Progress;
        }

        private PlayerProgress NewProgress()
        {
            var progress =  new PlayerProgress();
            return progress;
        }
    }
}