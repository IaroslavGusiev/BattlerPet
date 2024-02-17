using Code.Gameplay.Entity;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class EntityAction
    {
        public IEntity Caster;
        public List<string> TargetIds;
        public SkillType SkillType;
        public SkillModifier SkillModifier;
    }
}