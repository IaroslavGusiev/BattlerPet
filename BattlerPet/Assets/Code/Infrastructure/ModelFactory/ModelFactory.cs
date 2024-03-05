using Code.Data;
using System.Linq;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class ModelFactory
    {
        public EntityModel CreateHeroModel(EntityConfig config, string entityId)
        {
            var dto = new EntityModelDTO // TODO: logic to save and load
            {
                EntityId = entityId,
                MaxHealth = config.MaxHp,
                MaxHaste = config.MaxHaste,
                EntityType = (byte) config.EntityType
            };
            
            return new EntityModel(dto, CreateSkillModels(config));
        }

        private List<SkillModel> CreateSkillModels(EntityConfig config) => 
            config.SkillConfigs.Select(skillData => new SkillModel(skillData)).ToList();
    }
}