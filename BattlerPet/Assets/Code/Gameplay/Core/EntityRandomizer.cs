using Code.Data.Battlefield;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class EntityRandomizer
    {
        private readonly Dictionary<SideType, List<EntityType>> _entitiesMapping = new();

        public EntityRandomizer() // TODO: model 
        {
            var heroTypes = new List<EntityType>()
            {
                EntityType.ElvenArcher,
                EntityType.Barbarian,
                EntityType.Knight,
                EntityType.Wizard
            };

            var monsterTypes = new List<EntityType>()
            {
                EntityType.Bat,
                EntityType.Orc,
                EntityType.Dragon,
                EntityType.EvilMage,
                EntityType.Skeleton
            };

            _entitiesMapping.Add(SideType.PlayerSide, heroTypes);
            _entitiesMapping.Add(SideType.AISide, monsterTypes);
        }

        public IEnumerable<EntityType> GenerateRandomEntitiesForSide(SideType sideType, int amountForSide)
        {
            List<EntityType> entityTypes = _entitiesMapping[sideType];
            return entityTypes.PickRandom(amountForSide);
        }
    }
}