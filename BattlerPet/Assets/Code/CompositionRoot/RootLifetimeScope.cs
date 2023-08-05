using Code.Data;
using VContainer;
using UnityEngine;
using VContainer.Unity;

namespace Code.CompositionRoot
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private CorePrefabsData _corePrefabsData;
        private InstallerFactory _installerFactory;

        protected override void Configure(IContainerBuilder builder)
        {
            _installerFactory = new InstallerFactory();
            CreateGameStateMachineInstaller();
            CreateInfrastructureInstaller();
            CreateMonoBehaviourInstaller();
            InstallPlayerContextInstaller();
            InstallServiceInstaller();
            InstallUIInstaller();
            ConfigureAllCreatedInstallers(builder);
        }

        private void CreateGameStateMachineInstaller() => 
            _installerFactory.Create<GameStateMachineInstaller>();

        private void CreateInfrastructureInstaller() => 
            _installerFactory.Create<InfrastructureInstaller>();

        private void CreateMonoBehaviourInstaller() => 
            _installerFactory.Create<MonoBehaviourInstaller>(_corePrefabsData);

        private void InstallPlayerContextInstaller() => 
            _installerFactory.Create<PlayerContextInstaller>();

        private void InstallServiceInstaller() => 
            _installerFactory.Create<ServiceInstaller>();

        private void InstallUIInstaller() => 
            _installerFactory.Create<UIInstaller>(_corePrefabsData);

        private void ConfigureAllCreatedInstallers(IContainerBuilder builder) => 
            _installerFactory.Installers.ForEach(x => x.Install(builder));
    }
}