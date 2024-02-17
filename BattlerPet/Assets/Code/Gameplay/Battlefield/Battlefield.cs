using UnityEngine;
using Code.Services;
using Code.Infrastructure;
using Code.Data.Battlefield;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    public class Battlefield : IBattlefield
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IBattlefieldFactory _battlefieldFactory;
        
        private SlotSetup _slotSetup;
        private BattlefieldConfig _battlefieldConfig;

        public Battlefield(IBattlefieldFactory battlefieldFactory, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _battlefieldFactory = battlefieldFactory;
        }

        public async UniTask Initialize()
        {
            GetBattlefieldConfig();
            BattlefieldBehaviour battlefieldBehaviour = await CreateBattlefieldBehaviour();
            await CreateBattlefieldGenerator(battlefieldBehaviour);
            CreateSlotSetup(battlefieldBehaviour.SlotBehaviours);
            SetSkyboxMaterial(_battlefieldConfig.SkyboxData.GetRandomSkyboxMaterial());
        }

        public IEnumerable<SlotBehaviour> GetSlotForSide(SideType side) => 
            _slotSetup.GetSlotForSide(side);

        private void GetBattlefieldConfig() => 
            _battlefieldConfig = _staticDataService.GetBattlefieldConfig();

        private async UniTask<BattlefieldBehaviour> CreateBattlefieldBehaviour()
        {
            BattlefieldBehaviour battlefieldBehaviour = await _battlefieldFactory.CreateBattlefieldBehaviour(_battlefieldConfig.BattlefieldBehaviourPrefabAddress);
            battlefieldBehaviour.Initialize();
            return battlefieldBehaviour;
        }

        private async UniTask CreateBattlefieldGenerator(BattlefieldBehaviour battlefieldBehaviour)
        {
            var battlefieldGenerator = new BattlefieldGenerator(battlefieldBehaviour, _battlefieldConfig.BattlefieldDataContainer, _assetProvider, _battlefieldFactory);
            await battlefieldGenerator.GenerateBattlefield();
        }

        private void CreateSlotSetup(IEnumerable<SlotBehaviour> slots) => 
            _slotSetup = new SlotSetup(slots); 

        private void SetSkyboxMaterial(Material skyboxMaterial) =>
            RenderSettings.skybox = skyboxMaterial;
    }
}