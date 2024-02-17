using System;
using VContainer;
using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Gameplay.Core;
using Code.Gameplay.Entity;
using Cysharp.Threading.Tasks;
using Code.StaticData.Gameplay;
using Code.Gameplay.Battlefield;

namespace Code.Infrastructure
{
    public class GameFactory : IEntityFactory, IBattlefieldFactory
    {
        private readonly IDeathService _deathService;
        
        private readonly ModelFactory _modelFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(IObjectResolver objectResolver, IAssetProvider assetProvider, IStaticDataService staticDataService, IDeathService deathService)
        {
            _assetProvider = assetProvider;
            _deathService = deathService;
            _objectResolver = objectResolver;
            _staticDataService = staticDataService;
            _modelFactory = new ModelFactory();
        }

        public async UniTask<EntityBehaviour> CreateEntity(EntityType entityType, Vector3 at, Quaternion rotation, Transform parent) 
        {
            string uniqueId = CreateUniqueId();
            EntityConfig config = _staticDataService.GetEntityData(entityType);
            EntityModel entityModel = _modelFactory.CreateHeroModel(config, uniqueId);
            var prefab = await _assetProvider.LoadAndGetComponent<EntityBehaviour>(config.PrefabAddress);
            EntityBehaviour entity = _objectResolver.Instantiate(prefab, at, rotation, parent);
            entity.Initialize(new EntityController(entityModel), uniqueId);
            _deathService.RegisterEntity(entityModel);
            return entity;
        }

        public async UniTask<BattlefieldBehaviour> CreateBattlefieldBehaviour(string prefabAddress)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<BattlefieldBehaviour>(prefabAddress);
            BattlefieldBehaviour battlefieldBehaviour = _objectResolver.Instantiate(prefab, null);
            return battlefieldBehaviour;
        }

        public async UniTask<Cube> CreateBattlefieldItem(string prefabAddress, Vector3 at, Transform parent)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<Cube>(prefabAddress);
            return _objectResolver.Instantiate(prefab, at, prefab.transform.rotation, parent);
        }

        private string CreateUniqueId() => 
            Guid.NewGuid().ToString();
    }
}