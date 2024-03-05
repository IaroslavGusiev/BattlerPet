using UniRx;
using Code.Data;
using System.Linq;
using CodeBase.Extensions;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class EntityModel : IReactiveModel, ISavedData<EntityModelDTO>
    {
        public IReadOnlyReactiveProperty<string> UponDeath { get; private set; }

        private readonly EntityModelDTO _modelDto;
        private readonly List<SkillModel> _skillModels;
        private readonly JournalEntryReporter _entryReporter;
        private readonly List<EntityAttribute> _entityAttributes = new();

        public EntityModel(EntityModelDTO modelDto, List<SkillModel> skillModels) // TODO: EntityModelDTO 
        {
            _modelDto = modelDto;
            _skillModels = skillModels;
            _entryReporter = new JournalEntryReporter();
            CreateAttribute(AttributeType.Health, _modelDto.MaxHealth, _modelDto.MaxHealth);
            CreateAttribute(AttributeType.Haste, _modelDto.MaxHaste, 0f);
            CreateUponDeathReactiveProperty(_modelDto.EntityId);
        }
        
        public EntityModelDTO GetDataToSave() => 
            ParseCurrentModelToDto();

        public IEnumerable<ISkillModel> GetReadySkills() => 
            _skillModels.Where(x => x.IsReady);

        public ISkillModel GetSkillModel(AttackType attackType) =>
            _skillModels.FirstOrDefault(x => x.AttackType == attackType);

        public IAttribute GetAttribute(AttributeType attributeType) => 
            _entityAttributes.FirstOrDefault(x => x.AttributeType == attributeType);

        public void TickSkillsCooldown(float deltaTime) => 
            _skillModels.ForEach(x => x.TickCooldown(deltaTime));

        public void IncreaseHealth(float value) => 
            ModifyAttribute(AttributeType.Health, AttributeOperation.Increase, value);

        public void IncreaseHaste(float value) => 
            ModifyAttribute(AttributeType.Haste, AttributeOperation.Increase, value);

        public void ReduceHealth(float value) => 
            ModifyAttribute(AttributeType.Health, AttributeOperation.Decrease, value);

        public void ReduceHaste(float value) => 
            ModifyAttribute(AttributeType.Haste, AttributeOperation.Decrease, value);

        public void SetHasteToZero() => 
            ModifyAttribute(AttributeType.Haste, AttributeOperation.Set, 0f);

        private void ModifyAttribute(AttributeType attributeType, AttributeOperation operation, float value)
        {
            EntityAttribute attribute = GetConcreteAttribute(attributeType);
            attribute.ModifyCurrentValue(value, operation);
            _entryReporter.AddEntry(attributeType, operation, value, attribute.CurrentValue.Value); 
        }

        private void CreateAttribute(AttributeType attributeType, float maxValue, float currentValue) => 
            _entityAttributes.Add(new EntityAttribute(attributeType, maxValue, currentValue));

        private EntityAttribute GetConcreteAttribute(AttributeType attributeType) => 
            _entityAttributes.FirstOrDefault(x => x.AttributeType == attributeType);

        private void CreateUponDeathReactiveProperty(string entityId)
        {
            UponDeath = GetConcreteAttribute(AttributeType.Health).CurrentValue
                .Select(stat => stat <= 0 ? entityId : default)
                .ToReactiveProperty(entityId);
        }

        private EntityModelDTO ParseCurrentModelToDto()
        {
            return _modelDto
                .With(x => x.EntityId = _modelDto.EntityId)
                .With(x => x.EntityType = _modelDto.EntityType)
                .With(x => x.CurrentHaste = GetConcreteAttribute(AttributeType.Haste).CurrentValue.Value)
                .With(x => x.CurrentHealth= GetConcreteAttribute(AttributeType.Health).CurrentValue.Value);
        }
    }

    public interface ISavedData<out TArg>
    {
        public TArg GetDataToSave();
    }
}