using Code.Data;
using UnityEngine;
using System.Collections.Generic;

namespace Code.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IEnumerable<IProgressSaver> _saverServices;
        private readonly IPlayerProgressService _playerProgressService;

        public SaveLoadService(IEnumerable<IProgressSaver> saverServices, IPlayerProgressService playerProgressService)
        {
            _saverServices = saverServices;
            _playerProgressService = playerProgressService;
        }

        public void SaveProgress()
        {
            foreach (IProgressSaver saver in _saverServices) 
                saver.UpdateProgress(_playerProgressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, _playerProgressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}