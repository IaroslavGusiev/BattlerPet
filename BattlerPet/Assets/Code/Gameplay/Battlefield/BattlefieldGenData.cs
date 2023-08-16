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

        private Dictionary<SideType, CubeData> _fenceData;

        public void WarmUp()
        {
            CubeData data = CubeData[CubeType.Fence].PickRandom();
            _fenceData = new Dictionary<SideType, CubeData>()
            {
                [SideType.HeroSide] = data,
                [SideType.EnemySide] = CubeData[CubeType.Fence].PickRandomExcluding(data)
            };
        }

        public string GetCubePrefabPath(CubeType cubeType) => 
            CubeData[cubeType].PickRandom().PrefabPath;

        public string GetFencePrefabPath(SideType sideType) => 
            _fenceData[sideType].PrefabPath;
    }
}