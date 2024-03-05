using Code.UI;
using Code.Data;
using VContainer;
using Code.Services;
using VContainer.Unity;
using Code.UI.LoadingCurtain;
using Code.UI.ScreenServiceSpace;

namespace Code.CompositionRoot
{
    public class UIInstaller : IInstaller
    {
        private readonly CorePrefabsData _corePrefabsData;
        private IContainerBuilder _builder;

        public UIInstaller(CorePrefabsData corePrefabsData) => 
            _corePrefabsData = corePrefabsData;

        public void Install(IContainerBuilder builder)
        {
            _builder = builder;
            RegisterUIFactory();
            RegisterLoadingCurtain();
            RegisterShowWindowService();
        }

        private void RegisterUIFactory()
        {
            _builder
                .Register<UIFactory>(Lifetime.Singleton)
                .As<IUIFactory>();
        }

        private void RegisterLoadingCurtain()
        {
            _builder.RegisterComponentInNewPrefab(_corePrefabsData.LoadingCurtainPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ILoadingCurtain>();
        }

        private void RegisterShowWindowService()
        {
            _builder.Register<ScreenService>(Lifetime.Singleton)
                .As<IScreenService>()
                .AsSelf();
        }
    }
}