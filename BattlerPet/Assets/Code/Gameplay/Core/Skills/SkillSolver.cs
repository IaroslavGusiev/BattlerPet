using Code.Services;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class SkillSolver : ISkillSolver
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IEntityRegister _entityRegister;
        private List<ISkillApplier> _appliers;
        
        private readonly SkillModifierSolver _skillModifierSolver;
        private List<SkillExecution> _skillExecutions = new(20);

        public SkillSolver(IStaticDataService staticDataService, IEntityRegister entityRegister)
        {
            _staticDataService = staticDataService;
            _entityRegister = entityRegister;
            _skillModifierSolver = new SkillModifierSolver(_staticDataService, _entityRegister);
            InitSkillAppliers();
        }

        public void ProcessEntityAction(EntityAction entityAction)
        {
            var skillExecution = new SkillExecution()
            {
                SkillType = entityAction.SkillType,
                SkillModifier = entityAction.SkillModifier,
                TargetIds = entityAction.TargetIds,
                Caster = entityAction.Caster,
                // RemainingTime = delay for skill
            };
            
            foreach (ISkillApplier applier in _appliers)
            {
                if (applier.SkillType == entityAction.SkillType)
                    applier.ApplySkill(skillExecution);

                if (entityAction.SkillModifier == null)
                    return;
                _skillModifierSolver.ApplyModifierWithChance(entityAction.SkillModifier, null);
            }
        }

        private void InitSkillAppliers()
        {
            _appliers = new List<ISkillApplier>
            {
                new HealSkillApplier(_staticDataService, _entityRegister),
                new DamageSkillApplier(_staticDataService, _entityRegister),
                new HasteBuffSkillApplier(_staticDataService, _entityRegister)
            };

            // foreach (ISkillApplier applier in _appliers) // TODO: move this when container is resolved
            //     applier.WarmUp();
        }
    }
}