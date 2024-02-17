using Code.Data;
using Code.Services;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure.GameStateMachineScope
{
    public class LoadPlayerProgressState : IState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerProgressProvider _playerProgress;
        private readonly IEnumerable<IProgressReader> _progressReaderServices;

        public LoadPlayerProgressState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService, IPlayerProgressProvider playerProgress, IEnumerable<IProgressReader> progressReaderServices)
        {
            _playerProgress = playerProgress;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _progressReaderServices = progressReaderServices;
        }

        public async UniTask Enter()
        {
            await LoadPlayerProgress();
        }

        public async UniTask Exit()
        {
            
        }

        private async UniTask LoadPlayerProgress()
        {
            PlayerProgress progress = await LoadProgressOrInitNew();
            NotifyProgressReaders(progress);
            await _gameStateMachine.Enter<BattleAreaState>();
        }

        private void NotifyProgressReaders(PlayerProgress progress)
        {
            foreach (IProgressReader reader in _progressReaderServices)
                reader.LoadProgress(progress);
        }

        private async UniTask<PlayerProgress> LoadProgressOrInitNew()
        {
            PlayerProgress progress = await _saveLoadService.LoadProgress();

            if (progress == null)
                return new PlayerProgress();

            _playerProgress.Progress = progress;
            return _playerProgress.Progress;
        }
    }
}