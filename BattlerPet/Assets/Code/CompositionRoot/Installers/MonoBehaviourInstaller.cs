using System;
using Code.Data;
using VContainer;
using VContainer.Unity;
using Code.Infrastructure;
using Code.Infrastructure.UpdateRunner;

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
            RegisterUpdateRunner();
            RegisterBootstrapper();
            RegisterCoroutineRunner();
        }

        private void RegisterBootstrapper()
        {
            _builder.RegisterComponentInNewPrefab(_corePrefabsData.BootstrapperPrefab, Lifetime.Singleton)
                .As<IInitializable, IDisposable>();
        }

        private void RegisterCoroutineRunner()
        {
            _builder.RegisterComponentInNewPrefab(_corePrefabsData.CoroutineRunnerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ICoroutineRunner, IDisposable>();
        }

        private void RegisterUpdateRunner()
        {
            _builder.Register<UpdateRunner>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}