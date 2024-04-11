using System.Linq;
using Code.Services;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class HasteBuffSkillApplier : SkillApplier
    {
        public override SkillType SkillType => SkillType.HasteBuff;

        public HasteBuffSkillApplier(IStaticDataService staticDataService, IEntityRegister entityRegister) : base(staticDataService, entityRegister) { }

        protected override void ProcessSkillExecution(SkillExecution skillExecution)
        {
            IEntity caster = GetCaster(skillExecution.Caster);
            SkillConfig skillConfig = GetSkillConfig(caster.EntityType, skillExecution.AttackType);
            
            foreach (IEntity selectedTarget in skillExecution.TargetIds
                         .Select(targetId => EntityRegister.GetEntity(targetId)))
                selectedTarget.IncreaseHaste(skillConfig.Value);
        }
    }
}