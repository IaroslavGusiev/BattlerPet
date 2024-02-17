using Code.Gameplay.Entity;
using Code.Services;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core.AI
{
    public class StupidAI
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
            // SkillType skillType = entity // TODO: here i need ready skills
            return default;
        }
        
        // public HeroAction MakeBestDecision(IHero readyHero)
        // {
        //     SkillTypeId chosen =
        //         readyHero.State.SkillStates
        //             .Where(x => x.IsReady)
        //             .Select(x => x.TypeId)
        //             .PickRandom();
        //
        //     HeroSkill skill = _staticDataService.HeroSkillFor(chosen, readyHero.TypeId);
        //
        //     return new HeroAction
        //     {
        //         Caster = readyHero,
        //         Skill = chosen,
        //         SkillKind = skill.Kind,
        //         TargetIds = ChoseTargets(readyHero.Id, skill.TargetType, _targetPicker.AvailableTargetsFor(readyHero.Id, skill.TargetType))
        //     };
        // }
        //
        // private List<string> ChoseTargets(string casterId, TargetType targetType, IEnumerable<string> availableTargets)
        // {
        //     switch (targetType)
        //     {
        //         case TargetType.Ally:
        //             return new List<string> {_heroRegistry.AlliesOf(casterId).PickRandom()};
        //         case TargetType.Enemy:
        //             return new List<string> {_heroRegistry.EnemiesOf(casterId).PickRandom()};
        //         case TargetType.AllAllies:
        //             return _heroRegistry.AlliesOf(casterId).ToList();
        //         case TargetType.AllEnemies:
        //             return _heroRegistry.EnemiesOf(casterId).ToList();
        //         case TargetType.Self:
        //             return new List<string> {casterId};
        //     }
        //
        //     return new List<string>();
        // }
    }
}