using Code.Data;
using UnityEngine;
using Code.Services;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class LoadPlayerProgressState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayerProgressService _playerProgress;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;

        public LoadPlayerProgressState(IGameStateMachine gameStateMachine, ISaveLoadService saveLoadService, IPlayerProgressService playerProgress, IEnumerable<IProgressReader> progressReaderServices)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _playerProgress = playerProgress;
            _progressReaderServices = progressReaderServices;
        }

        public void Enter()
        {
            Debug.Log("LoadPlayerProgressState enter"); 
            LoadPlayerProgress().Forget();
        }

        public void Exit() { }

        private async UniTaskVoid LoadPlayerProgress()
        {
            PlayerProgress progress = await LoadProgressOrInitNew();
            NotifyProgressReaderServices(progress);
            _gameStateMachine.Enter<LoadLevelState, SceneName>(payload: SceneName.BattleArea);
        }

        private void NotifyProgressReaderServices(PlayerProgress progress)
        {
            foreach (IProgressReader reader in _progressReaderServices)
                reader.LoadProgress(progress);
        }

        private async Task<PlayerProgress> LoadProgressOrInitNew()
        {
            PlayerProgress progress = await _saveLoadService.LoadProgress();

            if (progress == null)
                return new PlayerProgress();

            _playerProgress.Progress = progress;
            return _playerProgress.Progress;

        }
    }
}