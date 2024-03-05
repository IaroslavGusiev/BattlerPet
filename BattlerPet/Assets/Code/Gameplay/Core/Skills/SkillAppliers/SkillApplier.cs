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

        public void WarmUp()
        {
            
        }

        public void ApplySkill(SkillExecution skillExecution)
        {
            IEntity caster = GetCaster(skillExecution.Caster);

            foreach (string targetId in skillExecution.TargetIds)
                ApplySkillTo(caster, skillExecution, targetId);
        }

        protected abstract void ApplySkillTo(IEntity caster, SkillExecution skillExecution, string targetId);

        private IEntity GetCaster(string casterID) => 
            EntityRegister.GetEntity(casterID);
        
        // _battleTextPlayer.PlayText($"{skill.Value}", Color.red, target.transform.position);
        // PlayFx(skill.CustomTargetFx, target.transform.position);
    }
}