using VContainer;
using Code.Services;
using VContainer.Unity;

namespace Code.CompositionRoot
{
    public class PlayerContextInstaller : IInstaller
    {
        private IContainerBuilder _builder;
        
        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterSaveLoadService();
            RegisterPlayerProgressProvider();
        }
        
        private void RegisterSaveLoadService()
        {
            _builder.Register<SaveLoadService>(Lifetime.Singleton)
                .As<ISaveLoadService>();
        }

        private void RegisterPlayerProgressProvider()
        {
            _builder.Register<PlayerProgressProvider>(Lifetime.Singleton)
                .As<IPlayerProgressProvider>();
        }
    }
}