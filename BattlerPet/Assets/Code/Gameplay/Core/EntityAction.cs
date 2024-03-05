using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class EntityAction
    {
        public string Caster;
        public SkillType SkillType;
        public AttackType AttackType;
        public List<string> TargetIds;
        public SkillModifier SkillModifier;
    }
}