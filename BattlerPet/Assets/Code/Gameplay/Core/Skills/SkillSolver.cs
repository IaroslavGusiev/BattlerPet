using System.Linq;
using Code.Services;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class SkillSolver : ISkillSolver
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IEntityRegister _entityRegister;
        private List<ISkillApplier> _appliers;
        
        private readonly SkillModifierSolver _skillModifierSolver;
        private readonly List<SkillExecution> _skillExecutions = new(20); 

        public SkillSolver(IStaticDataService staticDataService, IEntityRegister entityRegister)
        {
            _staticDataService = staticDataService;
            _entityRegister = entityRegister;
            _skillModifierSolver = new SkillModifierSolver(_staticDataService, _entityRegister);
            InitSkillAppliers();
        }

        public void ProcessEntityAction(EntityAction entityAction)
        {
            IEntity casterEntity = _entityRegister.GetEntity(entityAction.Caster);
            CreateSkillExecution(entityAction, casterEntity.EntityType);
            casterEntity.ExecuteSkill(entityAction.AttackType);
            
            // BattleTextPlayer
        }
        
        public void SkillDelaysTick(float deltaTime)
        {
            for (int i = _skillExecutions.Count - 1; i >= 0; i--)
            {
                SkillExecution activeSkill = _skillExecutions[i];
                activeSkill.RemainingTime -= deltaTime; // TODO: prepare time then wait until attack is done
                
                if (activeSkill.RemainingTime <= 0)
                {
                    _skillExecutions.Remove(activeSkill);
                    if (_entityRegister.IsAlive(activeSkill.Caster))
                        ApplySkill(activeSkill);
                }
            }
        }

        private void CreateSkillExecution(EntityAction entityAction, EntityType casterEntityType)
        {
            SkillConfig skillConfig = _staticDataService.SkillConfigFor(casterEntityType, entityAction.AttackType);
            var execution = new SkillExecution
            {
                RemainingTime = skillConfig.ActualSkillAttackTime,
                SkillModifier = entityAction.SkillModifier,
                AttackType = entityAction.AttackType,
                SkillType = entityAction.SkillType,
                TargetIds = entityAction.TargetIds,
                Caster = entityAction.Caster
            };
            _skillExecutions.Add(execution);
        }
        
        private void ApplySkill(SkillExecution skillExecution)
        {
            foreach (ISkillApplier applier in _appliers
                         .Where(applier => applier.SkillType == skillExecution.SkillType))
                applier.ApplySkill(skillExecution);
            
            if (skillExecution.SkillModifier == null)
                return;
            _skillModifierSolver.ApplyModifierWithChance(skillExecution); // TODO: some chance service 
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