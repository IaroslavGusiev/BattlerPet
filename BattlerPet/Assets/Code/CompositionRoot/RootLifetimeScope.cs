using System;
using Code.UI;
using Code.Data;
using VContainer;
using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;
using Code.UI.LoadingCurtain;
using Code.UI.ScreenServiceSpace;
using Code.Infrastructure.UpdateRunner;
using Code.Infrastructure.GameStateMachineScope;

namespace Code.CompositionRoot
{
    public class RootLifetimeScope : SceneScope<AppStateMachine, AppBootstrapper>
    {
        [SerializeField] private CorePrefabsData _corePrefabsData;
        private IContainerBuilder _builder;

        protected override void OnConfigure(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterUIFactory();
            RegisterSceneLoader();
            RegisterUpdateRunner();
            RegisterAssetProvider();
            RegisterScreenService();
            RegisterLoadingCurtain();
            RegisterCoroutineRunner();
            RegisterSaveLoadService();
            RegisterStaticDataService();
            RegisterPlayerProgressProvider();
        }
        
        private void RegisterUIFactory()
        {
            _builder
                .Register<UIFactory>(Lifetime.Singleton)
                .As<IUIFactory>();
        }
        
        private void RegisterSceneLoader()
        {
            _builder
                .Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>()
                .AsSelf();
        }

        private void RegisterUpdateRunner()
        {
            _builder
                .Register<UpdateRunner>(Lifetime.Singleton)
                .As<ITickable, ITickSource>();
        }

        private void RegisterAssetProvider()
        {
            _builder
                .Register<AssetProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
        }
        
        private void RegisterScreenService()
        {
            _builder
                .Register<ScreenService>(Lifetime.Singleton)
                .As<IScreenService>()
                .AsSelf();
        }
        
        private void RegisterLoadingCurtain()
        {
            _builder
                .RegisterComponentInNewPrefab(_corePrefabsData.LoadingCurtainPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ILoadingCurtain>();
        }

        private void RegisterCoroutineRunner()
        {
            _builder
                .RegisterComponentInNewPrefab(_corePrefabsData.CoroutineRunnerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ICoroutineRunner, IDisposable>();
        }
        
        private void RegisterSaveLoadService()
        {
            _builder
                .Register<SaveLoadService>(Lifetime.Singleton)
                .As<ISaveLoadService>();
        }

        private void RegisterStaticDataService()
        {
            _builder
                .Register<StaticDataService>(Lifetime.Singleton)
                .AsSelf()
                .As<IStaticDataService>();
        }
        
        private void RegisterPlayerProgressProvider()
        {
            _builder
                .Register<PlayerProgressProvider>(Lifetime.Singleton)
                .As<IPlayerProgressProvider>();
        }
    }
}