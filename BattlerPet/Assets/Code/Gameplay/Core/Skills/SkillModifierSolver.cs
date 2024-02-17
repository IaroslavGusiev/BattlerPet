using Code.Services;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;
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

        public void ApplyModifierWithChance(SkillModifier modifier, IEntity target)
        {
            if (modifier != null && UnityEngine.Random.value <= modifier.Chance)
            {
                ApplyModifier(modifier, target);
            }
        }

        private void ApplyModifier(SkillModifier modifier, IEntity target)
        {
            foreach (ISkillModifierApplier modifierApplier in _modifierAppliers)
            {
                if (modifier.ModifierType == modifierApplier.SkillModifierType)
                {
                    modifierApplier.ApplyModifier();
                }
            }
        }
    }
}