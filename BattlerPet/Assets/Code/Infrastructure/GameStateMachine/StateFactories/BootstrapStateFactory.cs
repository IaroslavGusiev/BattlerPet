using Code.Services;
using System.Collections.Generic;

namespace Code.Infrastructure.GameStateMachine
{
    public class BootstrapStateFactory
    {
        private readonly StaticDataService _staticDataService;
        private readonly IShowWindowsService _showWindowsService;
        private readonly IEnumerable<IInitializeHandler> _initializeHandlers;

        public BootstrapStateFactory(StaticDataService staticDataService, IEnumerable<IInitializeHandler> initializeHandlers)
        {
            _initializeHandlers = initializeHandlers;
            _staticDataService = staticDataService;
        }

        public BootstrapState Create(IGameStateMachine gameStateMachine) => 
            new(gameStateMachine, _staticDataService, _initializeHandlers);
    }
}