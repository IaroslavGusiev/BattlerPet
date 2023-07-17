using Code.UI;
using VContainer;
using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;
using CodeBase.Extensions;
using Code.UI.LoadingCurtain;
using Code.Services.JSONSaver;
using Code.Infrastructure.GameFactory;
using Code.Infrastructure.GameStateMachine;

namespace Code.CompositionRoot
{
    public class RootLifetimeScope : LifetimeScope
    {
        #region CorePrefabs
        [SerializeField] private UIRoot _uiRootPrefab;
        [SerializeField] private Bootstrapper _bootstrapperPrefab;
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;
        [SerializeField] private CoroutineRunner _coroutineRunnerPrefab;
        #endregion

        #region ContainerBuilder
        private IContainerBuilder _builder;
        #endregion

        protected override void Configure(IContainerBuilder builder)
        {
            GetContainerBuilder(builder);
            RegisterBootstrapper();
            RegisterCoroutineRunner();
            RegisterLoadingCurtain();
            RegisterUIRoot();
            RegisterSceneLoader();
            RegisterGameStateMachine();
            RegisterStaticDataService();
            RegisterAdsService();
            RegisterSaveLoadService();
            RegisterPlayerProgressService();
            RegisterAssetProvider();
            RegisterGameFactory();
            RegisterJsonSaver();
        }

        private void GetContainerBuilder(IContainerBuilder builder) => 
            _builder = builder;

        private void RegisterBootstrapper()
        {
            _builder.RegisterComponentInNewPrefab(_bootstrapperPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();
        }

        private void RegisterCoroutineRunner()
        {
            _builder.RegisterComponentInNewPrefab(_coroutineRunnerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ICoroutineRunner>();
        }

        private void RegisterLoadingCurtain()
        {
            _builder.RegisterComponentInNewPrefab(_loadingCurtainPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ILoadingCurtain>();
        }

        private void RegisterUIRoot()
        {
            _builder.RegisterComponentInNewPrefab(_uiRootPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();
        }

        private void RegisterSceneLoader()
        {
            _builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
        }

        private void RegisterGameStateMachine()
        {
            new GameStateMachineFactory(_builder)
                .With(i => i.RegisterGameStateMachine())
                .With(i => i.RegisterStateFactories());
        }

        private void RegisterStaticDataService()
        {
            _builder.Register<StaticDataService>(Lifetime.Singleton)
                .As<IStaticDataService>();
        }

        private void RegisterAdsService()
        {
            _builder.Register<AdsService>(Lifetime.Singleton)
                .As<IAdsService>();
        }

        private void RegisterSaveLoadService()
        {
            _builder.Register<SaveLoadService>(Lifetime.Singleton)
                .As<ISaveLoadService>();
        }

        private void RegisterPlayerProgressService()
        {
            _builder.Register<PlayerProgressService>(Lifetime.Singleton)
                .As<IPlayerProgressService>();
        }

        private void RegisterAssetProvider()
        {
            _builder.Register<AssetProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
        }

        private void RegisterGameFactory()
        {
            _builder.Register<GameFactory>(Lifetime.Singleton)
                .As<IGameFactory>();
        }

        private void RegisterJsonSaver()
        {
            _builder.Register<JsonSaver>(Lifetime.Singleton)
                .As<IJsonSaver>();
        }
    }
}