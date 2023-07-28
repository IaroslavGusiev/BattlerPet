using VContainer;
using Code.Services;
using VContainer.Unity;

namespace Code.LifetimeScopes
{
    public class BattleAreaScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Test>(Lifetime.Scoped).AsImplementedInterfaces();
        }
    }
    
    public class Test : IInitializable
    {
        private ISaveLoadService _saveLoadService;

        public Test(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            _saveLoadService.SaveProgress();
        }
    }
}