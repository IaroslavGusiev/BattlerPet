using System;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    [Serializable]
    public class BattlefieldPartBundle
    {
        public BattlefieldPart PartType;
        public List<BattlefieldPartData> BattlefieldPartData;
    }
}