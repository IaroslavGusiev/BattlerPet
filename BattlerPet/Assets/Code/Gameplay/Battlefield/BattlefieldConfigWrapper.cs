using UnityEngine;
using Code.Data.Battlefield;
using Code.Data.Gameplay.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class BattlefieldConfigWrapper
    {
        private readonly BattlefieldConfig _battlefieldConfig;
        
        private const int FenceCreationOffset = 1;
        private const int GridSizeOfTopLayer = 1;

        public BattlefieldConfigWrapper(BattlefieldConfig battlefieldConfig) => 
            _battlefieldConfig = battlefieldConfig;

        public int GetYValueForStartOfBotLayer() =>
            _battlefieldConfig.StartPositionOffset.y;

        public int GetYValueForEndOfTopLayer() =>
            _battlefieldConfig.GridSize.y;

        public int GetYValueForStartOfTopLayer() =>
            _battlefieldConfig.GridSize.y - GridSizeOfTopLayer;
        
        public int GetGridSizeX() => 
            _battlefieldConfig.GridSize.x;
        
        public int GetGridSizeZ() => 
            _battlefieldConfig.GridSize.z;

        public Vector3Int GetStartPosition() => 
            _battlefieldConfig.StartPositionOffset;

        public int GetEndXValueForFenceLane(int startXValue) => 
            GetGridSizeX() + startXValue;

        public int GetEndZValueForDecorLane() => 
            GetStartPosition().z + GetGridSizeZ() - FenceCreationOffset;
        
        public Vector3Int GetStartCreationVectorForFence(SideType sideType)
        {
            return new Vector3Int
            {
                x = _battlefieldConfig.StartPositionOffset.x,
                y = _battlefieldConfig.GridSize.y,
                z = GetZValueForFence(sideType)
            };
        }

        public Vector3Int GetStartCreationVectorForDecor(SideType sideType)
        {
            return new Vector3Int
            {
                x = GetXValueForDecor(sideType),
                y = _battlefieldConfig.GridSize.y,
                z = _battlefieldConfig.StartPositionOffset.z + FenceCreationOffset
            };
        }

        private int GetZValueForFence(SideType sideType)
        {
            if (sideType == SideType.HeroSide)
                return _battlefieldConfig.StartPositionOffset.z;

            return _battlefieldConfig.StartPositionOffset.z + _battlefieldConfig.GridSize.z - FenceCreationOffset;
        }

        private int GetXValueForDecor(SideType sideType)
        {
            if (sideType == SideType.HeroSide)
                return _battlefieldConfig.StartPositionOffset.x;

            return _battlefieldConfig.StartPositionOffset.x + _battlefieldConfig.GridSize.x - FenceCreationOffset;
        }
    }
}