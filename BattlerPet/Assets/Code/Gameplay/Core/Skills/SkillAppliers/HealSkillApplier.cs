using Code.Services;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class HealSkillApplier : SkillApplier
    {
        public override SkillType SkillType => SkillType.Heal;

        public HealSkillApplier(IStaticDataService staticDataService, IEntityRegister entityRegister) : base(staticDataService, entityRegister) { }

        protected override void ApplySkillTo(IEntity caster, SkillExecution skillExecution, string targetId)
        {
            IEntity selectedTarget = EntityRegister.GetEntity(targetId);
            SkillConfig skillConfig = StaticDataService.SkillConfigFor(caster.EntityType, skillExecution.AttackType);
            selectedTarget.IncreaseHealth(skillConfig.Value);
        }
    }
}