using UniRx;
using System.Linq;
using CodeBase.Extensions;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class EntityModel : IReactiveModel
    {
        public IReadOnlyReactiveProperty<string> UponDeath { get; private set; }

        private readonly List<SkillModel> _skillModels;
        private readonly JournalEntryReporter _entryReporter;
        private readonly List<EntityAttribute> _entityAttributes = new();

        public EntityModel(string entityId, float maxHp, float maxHaste, List<SkillModel> skillModels)
        {
            _skillModels = skillModels;
            _entryReporter = new JournalEntryReporter();
            CreateAttribute(AttributeType.Health, maxHp, maxHp);
            CreateAttribute(AttributeType.Haste, maxHaste, 0f);
            CreateUponDeathReactiveProperty(entityId);
        }

        public IEnumerable<ISkillModel> GetReadySkills()
        {
            return _skillModels.Where(x => x.IsReady);
        }

        public IAttribute GetAttribute(AttributeType attributeType) => 
            _entityAttributes.FirstOrDefault(x => x.AttributeType == attributeType);

        public void TickSkillsCooldown(float deltaTime) => 
            _skillModels.ForEach(x => x.TickCooldown(deltaTime));

        public void TakeDamage(float incomeDamage)
        {
            EntityAttribute healthAttribute = GetConcreteAttribute(AttributeType.Health);
            _entryReporter.AddEntry<TakeDamageJournalEntry>(incomeDamage, healthAttribute.CurrentValue.Value);
            healthAttribute.DecreaseCurrentValue(incomeDamage);
        }

        public void UpdateHaste(float amountToAdd)
        {
            EntityAttribute hasteAttribute = GetConcreteAttribute(AttributeType.Haste);
            _entryReporter.AddEntry<HasteJournalEntry>(amountToAdd, hasteAttribute.CurrentValue.Value);
            hasteAttribute.IncreaseCurrentValue(amountToAdd);
        }

        private void CreateUponDeathReactiveProperty(string entityId)
        {
            UponDeath = GetConcreteAttribute(AttributeType.Health).CurrentValue
                .Select(stat => stat <= 0 ? entityId : default)
                .ToReactiveProperty(entityId);
        }

        private void CreateAttribute(AttributeType attributeType, float maxValue, float currentValue)
        {
            new EntityAttribute(attributeType, maxValue, currentValue)
                .With(x => _entityAttributes.Add(x));
        }

        private EntityAttribute GetConcreteAttribute(AttributeType attributeType) => 
            _entityAttributes.FirstOrDefault(x => x.AttributeType == attributeType);
    }
}