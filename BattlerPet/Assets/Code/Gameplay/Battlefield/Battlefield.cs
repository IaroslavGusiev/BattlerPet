using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;
using CodeBase.Extensions;
using Code.Data.Gameplay.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class Battlefield : IInitializable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IBattlefieldFactory _battlefieldFactory;
        private BattlefieldConfig _battlefieldConfig;

        public Battlefield(IBattlefieldFactory battlefieldFactory, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _battlefieldFactory = battlefieldFactory;
        }

        public void Initialize()
        {
            GetBattlefieldConfig();
            BattlefieldBehaviour battlefieldBehaviour = CreateBattlefieldBehaviour();
            CreateBattlefieldGenerator(battlefieldBehaviour);
            SetSkyboxMaterial(_battlefieldConfig.SkyboxData.GetRandomSkyboxMaterial());
        }

        private void GetBattlefieldConfig() => 
            _battlefieldConfig = _staticDataService.GetBattlefieldConfig();

        private BattlefieldBehaviour CreateBattlefieldBehaviour()
        {
            return _battlefieldFactory
                .CreateBattlefieldBehaviour(_battlefieldConfig.BattlefieldBehaviourPath)
                .With(x => x.Initialize());
        }

        private void CreateBattlefieldGenerator(BattlefieldBehaviour battlefieldBehaviour) => 
            new BattlefieldGenerator(battlefieldBehaviour, _battlefieldConfig.BattlefieldDataContainer, _assetProvider, _battlefieldFactory).GenerateBattlefield();

        private void SetSkyboxMaterial(Material skyboxMaterial) =>
            RenderSettings.skybox = skyboxMaterial;
    }
}