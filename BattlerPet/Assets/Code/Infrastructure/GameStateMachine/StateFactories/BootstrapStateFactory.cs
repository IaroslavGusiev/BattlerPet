using Code.Services;

namespace Code.Infrastructure.GameStateMachine
{
    public class BootstrapStateFactory
    {
        private readonly IAdsService _adsService;
        private readonly IStaticDataService _staticDataService;

        public BootstrapStateFactory(IAdsService adsService, IStaticDataService staticDataService)
        {
            _adsService = adsService;
            _staticDataService = staticDataService;
        }

        public BootstrapState Create(IGameStateMachine gameStateMachine) => 
            new(gameStateMachine, _adsService, _staticDataService);
    }
}