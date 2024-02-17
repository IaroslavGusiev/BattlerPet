using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public interface ISkillModel
    {
        bool IsReady { get; }
        float MaxCooldown { get; }
        AttackType AttackType { get; }
    }
}