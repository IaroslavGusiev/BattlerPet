using Code.Services;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class DamageSkillApplier : ISkillApplier
    {
        public SkillType SkillType => SkillType.Damage;
        
        private readonly IStaticDataService _staticDataService;
        private readonly IEntityRegister _entityRegister;

        public DamageSkillApplier(IStaticDataService staticDataService, IEntityRegister entityRegister)
        {
            _staticDataService = staticDataService;
            _entityRegister = entityRegister;
        }

        public void WarmUp()
        {
            
        }

        public void ApplySkill(SkillExecution skillExecution)
        {
            
        }
    }
}