using UnityEngine;
using Code.Data.Battlefield;
using System.Collections.Generic;
using Code.Data.Gameplay.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class BattlefieldGenData
    {
        public Material SkyBoxMaterial;
        public BattlefieldConfig BattlefieldConfig;
        public Dictionary<CubeType, List<CubeData>> CubeData = new();

        public string GetCubePath(CubeType cubeType)
        {
            return CubeData[cubeType].PickRandom().PrefabPath;
        }
    }
}