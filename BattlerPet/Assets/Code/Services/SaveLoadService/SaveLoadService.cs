using Code.Data;
using Code.Services.JSONSaver;
using System.Collections.Generic;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IJsonSaver _jsonSaver;
        private readonly IEnumerable<IProgressSaver> _saverServices;
        private readonly IPlayerProgressService _playerProgressService;

        public SaveLoadService(IJsonSaver jsonSaver, IEnumerable<IProgressSaver> saverServices, IPlayerProgressService playerProgressService)
        {
            _jsonSaver = jsonSaver;
            _saverServices = saverServices;
            _playerProgressService = playerProgressService;
        }

        public void SaveProgress()
        {
            foreach (IProgressSaver saver in _saverServices) 
                saver.UpdateProgress(_playerProgressService.Progress);

            _jsonSaver.SaveData(ProgressKey, _playerProgressService.Progress);
        }

        public PlayerProgress LoadProgress() =>
            _jsonSaver.LoadData<PlayerProgress>(ProgressKey);
    }
}