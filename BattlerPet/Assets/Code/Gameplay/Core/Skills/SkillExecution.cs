using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class SkillExecution
    {
        public string Caster;
        public List<string> TargetIds;
        public SkillType SkillType;
        public AttackType AttackType; // TODO: maybe rename to skill index or number 
        public SkillModifier SkillModifier;
        public float RemainingTime;
    }
}