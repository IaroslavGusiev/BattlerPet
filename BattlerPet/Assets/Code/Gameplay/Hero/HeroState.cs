﻿using System.Collections.Generic;

namespace Code.Gameplay.Hero
{
    public class HeroState
    {
        public float MaxHp;
        public float CurrentHp;
        public float CurrentHaste;
        public float MaxHaste;
        public List<SkillState> SkillStates;
    }
}