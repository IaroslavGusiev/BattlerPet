using System.Linq;
using Code.Services;
using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core.AI
{
    public class StupidAI : IArtificialIntelligence
    {
        private readonly ITargetChooser _targetChooser;
        private readonly IEntityRegister _entityRegister;
        private readonly IStaticDataService _staticDataService;

        public StupidAI(ITargetChooser targetChooser, IEntityRegister entityRegister, IStaticDataService staticDataService)
        {
            _targetChooser = targetChooser;
            _entityRegister = entityRegister;
            _staticDataService = staticDataService;
        }

        public EntityAction MakeBestDecision(IEntity entity)
        {
            AttackType attackType = entity.GetReadySkills().PickRandom().AttackType;
            SkillConfig config = _staticDataService.SkillConfigFor(entity.EntityType, attackType);

            return new EntityAction
            {
                Caster = entity.Id,
                SkillType = config.SkillType,
                AttackType = config.AttackType,
                SkillModifier = config.SkillModifier,
                TargetIds = SelectTargets(entity.Id, config.TargetType, _targetChooser.AvailableTargetsFor(entity.Id, config.TargetType))
            };
            
        }

        private List<string> SelectTargets(string casterId, TargetType targetType, IEnumerable<string> availableTargets)
        {
            switch (targetType)
            {
                case TargetType.Self:
                    return new List<string> { casterId };
                
                case TargetType.Enemy:
                case TargetType.Ally:
                    return new List<string> { availableTargets.PickRandom() };
                
                case TargetType.AllEnemies:
                case TargetType.AllAllies:
                    return availableTargets.ToList();
            }
            return default;
        }
    }
}