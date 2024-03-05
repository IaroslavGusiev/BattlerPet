using System;
using UnityEngine;
using Code.Infrastructure;
using Code.Gameplay.Entity;
using Code.Data.Battlefield;
using Cysharp.Threading.Tasks;
using Code.StaticData.Gameplay;
using Code.Gameplay.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class BattleStarter 
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEntityRegister _entityRegister;
        private readonly EntityRandomizer _entityRandomizer;
        private readonly IBattleTurnService _battleTurnService;

        public BattleStarter(IEntityFactory entityFactory, IEntityRegister entityRegister, EntityRandomizer entityRandomizer, IBattleTurnService battleTurnService)
        {
            _entityFactory = entityFactory;
            _entityRegister = entityRegister;
            _entityRandomizer = entityRandomizer;
            _battleTurnService = battleTurnService;
        }

        public async UniTask Initialize(IBattlefield battlefield)
        {
            foreach (SideType sideType in Enum.GetValues(typeof(SideType))) // TODO: to small methods
            {
                if (sideType == SideType.None)
                    continue;
                
                var slots = new Queue<SlotBehaviour>(battlefield.GetSlotForSide(sideType));
                IEnumerable<EntityType> randomTypes = _entityRandomizer.GenerateRandomEntitiesForSide(sideType, 3);
                
                Transform entityParent = CreateEntityParent(sideType);
                
                foreach (EntityType entityType in randomTypes)
                {
                    SlotBehaviour slot = slots.Dequeue();
                    EntityBehaviour entity = await _entityFactory.CreateEntity(entityType, slot.GetPosition(), slot.GetRotation(), entityParent);
                    _entityRegister.AddEntityToTeam(entity, sideType);
                }
            }
            
            _battleTurnService.StartBattle();
        }

        private Transform CreateEntityParent(SideType sideType) => 
            new GameObject($"Entities_{sideType}").transform;
    }
}