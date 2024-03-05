using Code.Services;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class SkillModifierSolver
    {
        private readonly List<ISkillModifierApplier> _modifierAppliers;

        public SkillModifierSolver(IStaticDataService staticDataService, IEntityRegister entityRegister)
        {
            _modifierAppliers = new List<ISkillModifierApplier>()
            {
                new PoisonSkillModifierApplier(staticDataService, entityRegister),
                new HasteBurnSkillModifierApplier(staticDataService, entityRegister)
            };
        }

        public void ApplyModifierWithChance(SkillExecution skillExecution)
        {
            foreach (ISkillModifierApplier skillModifierApplier in _modifierAppliers)
               skillModifierApplier.ApplyModifier(skillExecution);
        }
    }
}