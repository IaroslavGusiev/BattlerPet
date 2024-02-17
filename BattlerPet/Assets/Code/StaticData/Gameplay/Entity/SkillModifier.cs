using System;

namespace Code.StaticData.Gameplay
{
    [Serializable]
    public class SkillModifier
    {
        public SkillModifierType ModifierType;
        public float Chance;
        public float Value;
        public float Amount;
    }
}