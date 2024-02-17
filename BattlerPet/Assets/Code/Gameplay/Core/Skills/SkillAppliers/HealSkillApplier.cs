using Code.Services;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class HealSkillApplier : ISkillApplier
    {
        public SkillType SkillType => SkillType.Heal;
        
        private readonly IStaticDataService _staticDataService;
        private readonly IEntityRegister _entityRegister;

        public HealSkillApplier(IStaticDataService staticDataService, IEntityRegister entityRegister)
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