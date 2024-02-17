using CodeBase.Extensions;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class SkillModel : ISkillModel
    {
        private float _cooldown;
        public float MaxCooldown { get; private set; }
        public AttackType AttackType { get; private set; }

        public SkillType SkillType;
        public SkillModifier SkillModifier;
        
        public bool IsReady => _cooldown <= 0;

        public SkillModel(SkillConfig config)
        {
            config
                .With(x => MaxCooldown = x.Cooldown)
                .With(x => AttackType = x.AttackType)
                .With(x => SkillType = x.SkillType)
                .With(x => SkillModifier = x.SkillModifier);
        }

        public void PutOnCooldown()
        {
            _cooldown = MaxCooldown;
        }

        public void TickCooldown(float delta)
        {
            if (_cooldown > 0)
                _cooldown -= delta;
        }
    }
}