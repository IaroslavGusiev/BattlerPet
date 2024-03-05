using Code.Services;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class HasteBurnSkillModifierApplier : ISkillModifierApplier
    {
        public SkillModifierType SkillModifierType => SkillModifierType.HasteBurn;
        
        private readonly IEntityRegister _entityRegister;
        private readonly IStaticDataService _staticDataService;
        
        public HasteBurnSkillModifierApplier(IStaticDataService staticDataService, IEntityRegister entityRegister)
        {
            _staticDataService = staticDataService;
            _entityRegister = entityRegister;
        }

        public void ApplyModifier(SkillExecution skillExecution)
        {
            // TODO: foreach
        }
    }
}