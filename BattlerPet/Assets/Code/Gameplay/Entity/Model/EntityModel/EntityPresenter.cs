using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class EntityPresenter : IEntity
    {
        public string Id { get; }
        public EntityType EntityType { get; }
        
        private readonly EntityModel _entityModel;
        private readonly EntityBehaviour _behaviour;
        
        // private readonly SkillComponent _skillComponent; TODO: implement

        public EntityPresenter(EntityModel entityModel, EntityBehaviour behaviour, EntityType entityType, string id) // TODO: EntityType entityType, string id move this to some class
        {
            Id = id;
            EntityType = entityType;
            _behaviour = behaviour;
            _entityModel = entityModel;
        }

        public void IncreaseHealth(float value) => 
            _entityModel.IncreaseHealth(value);

        public void IncreaseHaste(float amountToAdd) => 
            _entityModel.IncreaseHaste(amountToAdd);

        public void ReduceHealth(float incomeDamage) => 
            _entityModel.ReduceHealth(incomeDamage);

        public void ReduceHaste(float value) => 
            _entityModel.ReduceHaste(value);

        public void TickSkillsCooldown(float deltaTime) => 
            _entityModel.TickSkillsCooldown(deltaTime);

        public IEnumerable<ISkillModel> GetReadySkills() => 
            _entityModel.GetReadySkills();

        public void ResetHasteToZero() => 
            _entityModel.SetHasteToZero();

        public void EnableTurnIndicator(bool enable) => 
            _behaviour.EnableTurnIndicator(enable);

        public void AnimateDeath() => 
            _behaviour.AnimateDeath();

        public bool IsReadyForNextTick(float hasteTickValue)
        {
            IAttribute hasteAttribute = _entityModel.GetAttribute(AttributeType.Haste);
            return hasteAttribute.CurrentValue.Value + hasteTickValue >= hasteAttribute.MaxValue.Value;
        }

        public bool IsReadyForTurn()
        {
            IAttribute hasteAttribute = _entityModel.GetAttribute(AttributeType.Haste);
            return hasteAttribute.CurrentValue.Value >= hasteAttribute.MaxValue.Value;
        }

        public void ExecuteSkill(AttackType attackType)
        {
            _entityModel
                .GetSkillModel(attackType)
                .PutOnCooldown();
            
            _behaviour.AnimateSKill(attackType);
        }
    }
}