using Code.Data;
using VContainer;
using UnityEngine;
using Code.Infrastructure;
using Code.Infrastructure.GameStateMachineScope;

namespace Code.CompositionRoot
{
    public class RootLifetimeScope : SceneScope<AppStateMachine, AppBootstrapper>
    {
        [SerializeField] private CorePrefabsData _corePrefabsData;

        protected override void OnConfigure(IContainerBuilder builder)
        {
            new InfrastructureInstaller()
                .Install(builder);
            
            new MonoBehaviourInstaller(_corePrefabsData)
                .Install(builder);
            
            new UIInstaller(_corePrefabsData)
                .Install(builder);

            new PlayerContextInstaller()
                .Install(builder);

            new ServiceInstaller()
                .Install(builder);
        }
    }
}