using Code.Services;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public abstract class SkillApplier : ISkillApplier
    {
        public abstract SkillType SkillType { get; }

        protected readonly IStaticDataService StaticDataService;
        protected readonly IEntityRegister EntityRegister;

        protected SkillApplier(IStaticDataService staticDataService, IEntityRegister entityRegister)
        {
            EntityRegister = entityRegister;
            StaticDataService = staticDataService;
        }

        public void WarmUp() { }

        public void ApplySkill(SkillExecution skillExecution) => 
            ProcessSkillExecution(skillExecution);

        protected abstract void ProcessSkillExecution(SkillExecution skillExecution);

        protected IEntity GetCaster(string casterID) => 
            EntityRegister.GetEntity(casterID);
        
        protected SkillConfig GetSkillConfig(EntityType entityType, AttackType attackType) =>  
            StaticDataService.SkillConfigFor(entityType, attackType);

        // TODO: calculate skill value
    }
}