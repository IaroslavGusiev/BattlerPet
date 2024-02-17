using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public interface IEntity : ITurnBasedEntity, IDeath, IAttributeHandler
    {
        public string Id { get; }
        EntityType EntityType { get; }
        
        void TickSkillsCooldown(float deltaTime);
        bool IsReadyForNextTick(float hasteTickValue);
    }
}