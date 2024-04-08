using Code.Data;
using Cysharp.Threading.Tasks;
using Code.Services.JSONSaver;
using System.Collections.Generic;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService // TODO: rename with mention that it's player data
    {
        private readonly ISaver _saver;
        private readonly IEnumerable<IProgressSaver> _saverServices;
        private readonly IPlayerProgressProvider _playerProgressProvider;

        public SaveLoadService(IEnumerable<IProgressSaver> saverServices, IPlayerProgressProvider playerProgressProvider)
        {
            _saver = new JsonSaver();
            _saverServices = saverServices;
            _playerProgressProvider = playerProgressProvider;
        }

        public async UniTask SaveProgress()
        {
            foreach (IProgressSaver saver in _saverServices) 
                saver.UpdateProgress(_playerProgressProvider.Progress);

            await _saver.SaveData(relativePath: SavedKeysData.PlayerProgressKey, data: _playerProgressProvider.Progress);
        }  

        public async UniTask<PlayerProgress> LoadProgress() => 
            await _saver.LoadData<PlayerProgress>(relativePath: SavedKeysData.PlayerProgressKey);
    }
}