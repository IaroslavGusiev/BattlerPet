using System.Collections.Generic;

namespace Code.Gameplay.Hero
{
    public class HeroModel
    {
        public float MaxHp;
        public float CurrentHp;
        public float CurrentHaste;
        public float MaxHaste;
        public List<SkillModel> SkillModels;
    }
}