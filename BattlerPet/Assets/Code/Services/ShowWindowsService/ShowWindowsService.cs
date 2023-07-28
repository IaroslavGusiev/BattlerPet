using Code.Data;
using UnityEngine;
using Code.Infrastructure;

namespace Code.Services
{
    public class ShowWindowsService : IShowWindowsService, IInitializeHandler
    {
        private ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public ShowWindowsService(ISceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            Debug.Log("<color=yellow>ShowWindowsService Initialize</color>");
        }

        public void ShowWindow(WindowType windowType)
        {
            
        }
    }
}