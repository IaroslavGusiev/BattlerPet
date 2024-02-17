using System.Collections.Generic;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class EntityController 
    {
        private readonly EntityModel _entityModel;

        public EntityController(EntityModel entityModel) => 
            _entityModel = entityModel;

        public IReactiveModel ReactiveModel => 
            _entityModel;

        public void TakeDamage(float incomeDamage) => 
            _entityModel.TakeDamage(incomeDamage);

        public void UpdateHaste(float amountToAdd) => 
            _entityModel.UpdateHaste(amountToAdd);

        public void TickSkillsCooldown(float deltaTime) => 
            _entityModel.TickSkillsCooldown(deltaTime);

        public IEnumerable<ISkillModel> GetReadySkills() => 
            _entityModel.GetReadySkills();

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