using Code.Data;
using Code.Services.JSONSaver;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly ISaver _saver;
        private readonly IEnumerable<IProgressSaver> _saverServices;
        private readonly IPlayerProgressService _playerProgressService;

        public SaveLoadService(IEnumerable<IProgressSaver> saverServices, IPlayerProgressService playerProgressService)
        {
            _saver = new JsonSaver();
            _saverServices = saverServices;
            _playerProgressService = playerProgressService;
        }

        public async UniTaskVoid SaveProgress()
        {
            foreach (IProgressSaver saver in _saverServices) 
                saver.UpdateProgress(_playerProgressService.Progress);

            await _saver.SaveData(relativePath: SavedKeysData.PlayerProgressKey, data: _playerProgressService.Progress);
        }  

        public async UniTask<PlayerProgress> LoadProgress()
        {
            var progress = await _saver.LoadData<PlayerProgress>(relativePath: SavedKeysData.PlayerProgressKey);
            return progress;
        }
    }
}