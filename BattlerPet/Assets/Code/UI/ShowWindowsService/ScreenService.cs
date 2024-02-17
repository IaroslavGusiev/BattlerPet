using Code.Data;
using UnityEngine;
using Code.Services;
using Code.Infrastructure;

namespace Code.UI.ShowWindowsService
{
    public class ScreenService : IScreenService 
    {
        private ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public ScreenService(ISceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            Debug.Log("<color=yellow>ShowWindowsService Initialize</color>");
        }

        public void ShowScreen(WindowType windowType) // TODO: make base screen 
        {
            
        }
    }
}