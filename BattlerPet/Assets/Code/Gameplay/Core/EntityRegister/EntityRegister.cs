using System;
using System.Linq;
using Code.Gameplay.Entity;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class EntityRegister : IEntityRegister
    {
        private readonly Dictionary<string, IEntity> _allEntities = new();
    
        private readonly Dictionary<SideType, List<string>> _sidesMapping = new()
        {
            [SideType.PlayerSide] = new List<string>(),
            [SideType.AISide] = new List<string>()
        };
        
        public IEntity GetEntity(string entityId) => 
            _allEntities.GetValueOrDefault(entityId);
        
        // public T GetEntity<T>(string entityId) where T : class
        // {
        //     IEntity entity = _allEntities.GetValueOrDefault(entityId);
        //     if (entity is T castedEntity)
        //         return castedEntity;
        //     throw new InvalidOperationException($"Entity with ID { entityId } does not implement the interface { typeof(T) }.");
        // }

        public void AddEntityToTeam(IEntity entity, SideType sideType)
        {
            _sidesMapping[sideType].Add(entity.Id);
            _allEntities[entity.Id] = entity;
        }

        public void Unregister(string entityId)
        {
            foreach (List<string> sideList in _sidesMapping.Values)
                sideList.Remove(entityId);
            
            _allEntities.Remove(entityId);
        }

        public IEnumerable<string> AlliesOf(string heroId) => 
            _sidesMapping.Values.FirstOrDefault(list => list.Contains(heroId));

        public IEnumerable<string> EnemiesOf(string heroId) => 
            _sidesMapping.Values.FirstOrDefault(list => !list.Contains(heroId));

        public IEnumerable<IEntity> AllActiveEntities() =>
            _allEntities.Values;
    }
}