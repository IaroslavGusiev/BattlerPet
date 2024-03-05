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

        public bool IsAlive(string entityId) => 
            _allEntities.ContainsKey(entityId);

        public IEnumerable<string> AlliesOf(string heroId) => 
            _sidesMapping.Values.FirstOrDefault(list => list.Contains(heroId));

        public IEnumerable<string> EnemiesOf(string heroId) => 
            _sidesMapping.Values.FirstOrDefault(list => !list.Contains(heroId));

        public IEnumerable<IEntity> AllActiveEntities() =>
            _allEntities.Values;

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
    }
}