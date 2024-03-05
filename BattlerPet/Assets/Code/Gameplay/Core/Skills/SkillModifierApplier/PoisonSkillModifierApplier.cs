using Code.Services;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public class PoisonSkillModifierApplier : ISkillModifierApplier
    {
        public SkillModifierType SkillModifierType => SkillModifierType.Poison;
        
        private readonly IStaticDataService _staticDataService;
        private readonly IEntityRegister _entityRegister;
        
        public PoisonSkillModifierApplier(IStaticDataService staticDataService, IEntityRegister entityRegister)
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