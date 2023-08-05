using Code.Data;
using VContainer;
using VContainer.Unity;
using Code.Infrastructure;

namespace Code.CompositionRoot
{
    public class MonoBehaviourInstaller : IInstaller
    {
        private readonly CorePrefabsData _corePrefabsData;
        private IContainerBuilder _builder;

        public MonoBehaviourInstaller(CorePrefabsData corePrefabsData) => 
            _corePrefabsData = corePrefabsData;

        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterBootstrapper();
            RegisterCoroutineRunner();
        }
        
        private void RegisterBootstrapper()
        {
            _builder.RegisterComponentInNewPrefab(_corePrefabsData.BootstrapperPrefab, Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
        
        private void RegisterCoroutineRunner()
        {
            _builder.RegisterComponentInNewPrefab(_corePrefabsData.CoroutineRunnerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ICoroutineRunner>();
        }
    }
}