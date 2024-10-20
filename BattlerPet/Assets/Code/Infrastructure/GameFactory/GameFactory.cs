using System;
using VContainer;
using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Gameplay.Core;
using Code.Gameplay.Entity;
using System.Threading.Tasks;
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

        public async UniTask<IEntity> CreateEntity(EntityType entityType, Vector3 at, Quaternion rotation, Transform parent) 
        {
            string uniqueId = CreateUniqueId();
            EntityConfig config = _staticDataService.EntityConfigFor(entityType);
            EntityModel entityModel = _modelFactory.CreateHeroModel(config, uniqueId);
            EntityBehaviour entityBehaviour = await CreateEntityBehaviour(config, entityModel, at, rotation, parent);
            _deathService.RegisterEntity(entityModel);
            return new EntityPresenter(entityModel, entityBehaviour, entityType, uniqueId); // TODO: service to save model data; 
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

        private async Task<EntityBehaviour> CreateEntityBehaviour(EntityConfig config, EntityModel entityModel, Vector3 at, Quaternion rotation, Transform parent)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<EntityBehaviour>(config.PrefabAddress);
            EntityBehaviour behaviour = _objectResolver.Instantiate(prefab, at, rotation, parent);
            return behaviour.Initialize(entityModel);
        }

        private string CreateUniqueId() => 
            Guid.NewGuid().ToString();
    }
}