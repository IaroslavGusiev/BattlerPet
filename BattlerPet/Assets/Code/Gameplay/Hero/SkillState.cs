using CodeBase.Extensions;
using Code.StaticData.Hero;

namespace Code.Gameplay.Hero
{
    public class SkillState
    {
        public string Name;
        public float Cooldown;
        public float MaxCooldown;
        
        public void PutOnCooldown()
        {
            Cooldown = MaxCooldown;
        }

        public void TickCooldown(float delta)
        {
            if (Cooldown > 0)
                Cooldown -= delta;
        }

        public static SkillState FromSkillData(SkillData heroData)
        {
            return new SkillState()
                .With(x => x.Name = heroData.Name)
                .With(x => x.Cooldown = heroData.Cooldown);
        }
    }
}