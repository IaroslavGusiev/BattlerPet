using Code.Services;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class HasteBuffSkillApplier : ISkillApplier
    {
        public SkillType SkillType => SkillType.HasteBuff;
        
        private readonly IStaticDataService _staticDataService;
        private readonly IEntityRegister _entityRegister;

        public HasteBuffSkillApplier(IStaticDataService staticDataService, IEntityRegister entityRegister)
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