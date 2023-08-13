using Code.Data;
using VContainer;
using VContainer.Unity;
using Code.UI.LoadingCurtain;
using Code.UI.ShowWindowsService;

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
            RegisterLoadingCurtain();
            RegisterShowWindowService();
        }
        
        private void RegisterShowWindowService()
        {
            _builder.Register<ShowWindowsService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
        
        private void RegisterLoadingCurtain()
        {
            _builder.RegisterComponentInNewPrefab(_corePrefabsData.LoadingCurtainPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<ILoadingCurtain>();
        }
    }
}