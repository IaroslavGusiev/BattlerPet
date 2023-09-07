using Code.Data.Gameplay;
using CodeBase.Extensions;
using Code.StaticData.Hero;

namespace Code.Gameplay.Hero
{
    public class SkillModel
    {
        public float Cooldown;
        public float MaxCooldown;
        public AttackType AttackType;

        public SkillModel(SkillData heroData)
        {
            heroData
                .With(x => MaxCooldown = x.Cooldown)
                .With(x => AttackType = x.AttackType);
        }

        public void PutOnCooldown()
        {
            Cooldown = MaxCooldown;
        }

        public void TickCooldown(float delta)
        {
            if (Cooldown > 0)
                Cooldown -= delta;
        }

        // public static SkillModel FromSkillData(SkillData heroData)
        // {
        //     return new SkillModel()
        //         .With(x => x.Cooldown = heroData.Cooldown)
        //         .With(x => x.AttackType = heroData.AttackType);
        // }
    }
}