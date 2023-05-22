﻿using Code.UI;
using VContainer;
using UnityEngine;
using VContainer.Unity;
using Code.Infrastructure;
using Code.Infrastructure.Services;
using Code.Infrastructure.GameStateMachine;

namespace Code.CompositionRoot
{
    public class RootLifetimeScope : LifetimeScope
    {
        #region CorePrefabs
        [SerializeField] private UIRoot _uiRootPrefab;
        [SerializeField] private Bootstrapper _bootstrapperPrefab;
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
            RegisterUIRoot();
            RegisterSceneLoader();

            _builder.Register<BootstrapStateFactory>(Lifetime.Singleton);
            builder.RegisterFactory<IGameStateMachine, BootstrapState>(container => container.Resolve<BootstrapStateFactory>().Create, Lifetime.Singleton);
            
            RegisterGameStateMachine();
            
            RegisterStaticDataService();
            RegisterAdsService();
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
            _builder.Register<GameStateMachine>(Lifetime.Singleton)
                .As<IGameStateMachine>();
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
    }
}