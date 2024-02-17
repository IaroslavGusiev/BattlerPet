using VContainer;
using Code.Services;
using VContainer.Unity;

namespace Code.CompositionRoot
{
    public class ServiceInstaller : IInstaller
    {
        private IContainerBuilder _builder;
        
        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterStaticDataService();
            RegisterAdsService();
        }
        
        private void RegisterStaticDataService()
        {
            _builder.Register<StaticDataService>(Lifetime.Singleton)
                .AsSelf()
                .As<IStaticDataService>();
        }

        private void RegisterAdsService()
        {
            _builder.Register<AdsService>(Lifetime.Singleton)
                .AsSelf()
                .As<IAdsService>();
        }
    }
}