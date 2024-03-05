using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public interface ISkillModel
    {
        void PutOnCooldown();
        float GetSkillAnimationPlayTime();
        AttackType AttackType { get; }
    }
}