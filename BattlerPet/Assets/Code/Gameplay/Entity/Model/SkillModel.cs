using CodeBase.Extensions;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class SkillModel : ISkillModel
    {
        public float MaxCooldown { get; private set; }
        public AttackType AttackType { private set; get; }

        public SkillType SkillType;
        public SkillModifier SkillModifier;
        private float _cooldown;
        private float _actualSkillAttackTime;

        public SkillModel(SkillConfig config)
        {
            config
                .With(x => SkillType = x.SkillType)
                .With(x => MaxCooldown = x.Cooldown)
                .With(x => AttackType = x.AttackType)
                .With(x => SkillModifier = x.SkillModifier)
                .With(x => _actualSkillAttackTime = x.ActualSkillAttackTime);
        }

        public float GetSkillAnimationPlayTime()
        {
            return _actualSkillAttackTime;
        }

        public bool IsReady => 
            _cooldown <= 0;

        public void PutOnCooldown() => 
            _cooldown = MaxCooldown;

        public void TickCooldown(float delta)
        {
            if (_cooldown > 0)
                _cooldown -= delta;
        }
    }
}