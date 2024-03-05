using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public interface ISkillModifierApplier
    {
        public SkillModifierType SkillModifierType { get; }
        void ApplyModifier(SkillExecution skillExecution);
    }
}