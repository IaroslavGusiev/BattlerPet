using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public interface IEntity : ITurnBasedEntity, IAttributeHandler, ISkillHandler, IDeath
    {
        public string Id { get; }
        EntityType EntityType { get; }
        
        void ExecuteSkill(AttackType attackType); 
    }
}