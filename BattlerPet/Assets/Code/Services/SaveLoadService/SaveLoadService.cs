using Code.Data;
using Code.Services.JSONSaver;
using System.Collections.Generic;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly ISaver _saver;
        private readonly IEnumerable<IProgressSaver> _saverServices;
        private readonly IPlayerProgressService _playerProgressService;

        public SaveLoadService(ISaver saver, IEnumerable<IProgressSaver> saverServices, IPlayerProgressService playerProgressService)
        {
            _saver = saver;
            _saverServices = saverServices;
            _playerProgressService = playerProgressService;
        }

        public void SaveProgress()
        {
            foreach (IProgressSaver saver in _saverServices) 
                saver.UpdateProgress(_playerProgressService.Progress);

            _saver.SaveData(relativePath: SavedKeysData.PlayerProgressKey, _playerProgressService.Progress);
        }

        public PlayerProgress LoadProgress()
        {
            var playerProgress = _saver.LoadData<PlayerProgress>(relativePath: SavedKeysData.PlayerProgressKey);
            return playerProgress;
        }
    }
}