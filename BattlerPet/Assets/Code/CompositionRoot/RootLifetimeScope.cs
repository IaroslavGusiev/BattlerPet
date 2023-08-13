using Code.Data;
using VContainer;
using UnityEngine;
using VContainer.Unity;

namespace Code.CompositionRoot
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private CorePrefabsData _corePrefabsData;

        protected override void Configure(IContainerBuilder builder)
        {
            new GameStateMachineInstaller()
                .Install(builder);
            
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