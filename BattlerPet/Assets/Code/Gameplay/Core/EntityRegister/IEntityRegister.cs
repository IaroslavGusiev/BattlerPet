using Code.Gameplay.Entity;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public interface IEntityRegister
    {
        bool IsAlive(string entityId);
        void Unregister(string entityId);
        IEntity GetEntity(string entityId);
        void AddEntityToTeam(IEntity entity, SideType sideType);

        IEnumerable<IEntity> AllActiveEntities();
        IEnumerable<string> AlliesOf(string heroId);
        IEnumerable<string> EnemiesOf(string heroId);
    }
}