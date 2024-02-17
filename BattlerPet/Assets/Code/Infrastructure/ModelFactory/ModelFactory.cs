using System.Linq;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class ModelFactory
    {
        public EntityModel CreateHeroModel(EntityConfig config, string entityId) => 
            new(entityId, config.MaxHp, config.MaxHaste, CreateSkillModels(config));

        private List<SkillModel> CreateSkillModels(EntityConfig config) => 
            config.SkillConfigs.Select(skillData => new SkillModel(skillData)).ToList();
    }
}