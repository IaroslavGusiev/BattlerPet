using System.Linq;
using UnityEngine;
using Code.Services;
using Code.Infrastructure;
using Code.Data.Battlefield;
using System.Collections.Generic;
using Code.Data.Gameplay.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class BattlefieldGenerator
    {
        private readonly IAssetProvider _assetProvider;
        private readonly BattlefieldBehaviour _battlefield;
        private readonly IBattlefieldFactory _battlefieldFactory;
        private readonly BattlefieldDataContainer _battlefieldDataContainer;
        
        private const int RandomChanceForDecor = 75;

        public BattlefieldGenerator(BattlefieldBehaviour battlefield, BattlefieldDataContainer battlefieldDataContainer, IAssetProvider assetProvider, IBattlefieldFactory battlefieldFactory)
        {
            _battlefield = battlefield;
            _assetProvider = assetProvider;
            _battlefieldFactory = battlefieldFactory;
            _battlefieldDataContainer = battlefieldDataContainer;
        }

        public void GenerateBattlefield()
        {
            SetBotCubesMaterial();
            SetTopCubesMaterial();
            CreateFenceForEachSide();
            CreateDecor();
        }

        private void SetBotCubesMaterial()
        {
            string path = GetDataFromBundle(BattlefieldPart.BotCube).PickRandom().MaterialPath;
            var material = _assetProvider.Get<Material>(path);
            _battlefield.SetBotCubesMaterials(material);
        }

        private void SetTopCubesMaterial()
        {
            TopLayerCubeType randomType = _battlefieldDataContainer.TopLayerCubeTypes.PickRandom();
            
            List<Material> materials = (
                from partData 
                in GetDataFromBundle(BattlefieldPart.TopCube) 
                where partData.TopLayerCubeType == randomType 
                select _assetProvider.Get<Material>(partData.MaterialPath)).ToList();
            
            _battlefield.SetTopCubesMaterials(materials);
        }

        private void CreateFenceForEachSide()
        {
            BattlefieldPartData heroSideFenceData = GetDataFromBundle(BattlefieldPart.Fence).PickRandom();
            BattlefieldPartData enemySideFenceData = GetDataFromBundle(BattlefieldPart.Fence).PickRandomExcluding(heroSideFenceData);
            
            CreateContainer("Fence", _battlefield.transform, out Transform container);
            
            SpawnFenceForLine(_battlefield.GetFenceSpawnPositions(SideType.HeroSide), heroSideFenceData.PrefabPath, container);
            SpawnFenceForLine(_battlefield.GetFenceSpawnPositions(SideType.EnemySide), enemySideFenceData.PrefabPath, container);
        }

        private void CreateDecor()
        {
            CreateContainer("Decor", _battlefield.transform, out Transform container);
            List<string> prefabsPaths = GetDataFromBundle(BattlefieldPart.Decor).Select(x => x.PrefabPath).ToList(); 
                
            foreach (Vector3 pos in _battlefield.GetDecorSpawnPositions().Where(_ => ShouldSpawnDecor()))
                _battlefieldFactory.CreateBattlefieldItem(prefabsPaths.PickRandom(), pos, container);
        }
        
        private IEnumerable<BattlefieldPartData> GetDataFromBundle(BattlefieldPart part)
        {
            BattlefieldPartBundle bundle = _battlefieldDataContainer.BattlefieldPartBundles.FirstOrDefault(x => x.PartType == part);
            return bundle?.BattlefieldPartData;
        }

        private void SpawnFenceForLine(List<Vector3> spawnPositions, string prefabPath, Transform container)
        {
            foreach (Vector3 position in spawnPositions)
                _battlefieldFactory.CreateBattlefieldItem(prefabPath, position, container);
        }

        private void CreateContainer(string name, Transform parent, out Transform container)
        {
            container = new GameObject(name).transform;
            container.SetParent(parent);
        }

        private bool ShouldSpawnDecor()
        {
            int randomChanceForDecor = Random.Range(0, 101);
            return randomChanceForDecor < RandomChanceForDecor;
        }
    }
}