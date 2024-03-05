using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class EntityController 
    {
        private readonly EntityModel _entityModel;

        public EntityController(EntityModel entityModel) => 
            _entityModel = entityModel;

        public IReactiveModel ReactiveModel => 
            _entityModel;
        
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

        public void PutSkillOnCooldown(AttackType attackType) => 
            _entityModel.GetSkillModel(attackType).PutOnCooldown();

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
    }
}