using System;
using UnityEngine;
using Code.Infrastructure;
using Code.Data.Battlefield;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Battlefield
{
    public class BattlefieldGenerator
    {
        private readonly IBattlefieldFactory _battlefieldFactory;
        private readonly BattlefieldConfigWrapper _configWrapper;
        private readonly BattlefieldGenData _genData;

        private Transform _cubesContainer;
        private Transform _fenceContainer;
        private Transform _decorContainer;

        private const int RandomChanceForDecor = 75;

        public BattlefieldGenerator(IBattlefieldFactory battlefieldFactory, BattlefieldGenData genData)
        {
            _genData = genData;
            _battlefieldFactory = battlefieldFactory;
            _configWrapper = new BattlefieldConfigWrapper(_genData.BattlefieldConfig);
        }

        public void Generate()
        {
            CreateContainersForObjects();
            GenerateBotAndTopLayers();
            GenerateFenceAndDecorLanes();
        }

        private void CreateContainersForObjects()
        {
            Transform battlefieldContainer = new GameObject(nameof(Battlefield)).transform;
            CreateContainer("Cubes", battlefieldContainer, out _cubesContainer);
            CreateContainer("Fence", battlefieldContainer, out _fenceContainer);
            CreateContainer("Decor", battlefieldContainer, out _decorContainer);
        }
        
        private void CreateContainer(string name, Transform parent, out Transform container)
        {
            container = new GameObject(name).transform;
            container.SetParent(parent);
        }

        private void GenerateBotAndTopLayers()
        {
            int yValueForStartOfBotLayer = _configWrapper.GetYValueForStartOfBotLayer();
            int yValueForStartOfTopLayer = _configWrapper.GetYValueForStartOfTopLayer();
            int yValueForEndOfTopLayer = _configWrapper.GetYValueForEndOfTopLayer();

            GenerateLayer(CubeType.BotLayer, yValueForStartOfBotLayer, yValueForStartOfTopLayer, _cubesContainer);
            GenerateLayer(CubeType.TopLayer, yValueForStartOfTopLayer, yValueForEndOfTopLayer, _cubesContainer);
        }

        private void GenerateFenceAndDecorLanes()
        {
            foreach (SideType sideType in Enum.GetValues(typeof(SideType)))
            {
                if (sideType == SideType.None)
                    continue;

                GenerateFenceLane(sideType, _fenceContainer);
                GenerateDecorLane(sideType, _decorContainer);
            }
        }

        private void GenerateLayer(CubeType cubeType, int startY, int endY, Transform parent)
        {
            for (int y = startY; y < endY; y++)
                GenerateLayerForY(y, parent, cubeType);
        }
        
        private void GenerateLayerForY(int y, Transform parent, CubeType cubeType)
        {
            for (int x = 0; x < _configWrapper.GetGridSizeX(); x++)
            {
                for (int z = 0; z < _configWrapper.GetGridSizeZ(); z++)
                {
                    Vector3 position = CalculateCubePosition(x, y, z);
                    CreateRequiredCube(cubeType, position, parent);
                }
            }
        }
        
        private void GenerateFenceLane(SideType sideType, Transform parent)
        {
            Vector3Int creationVector = _configWrapper.GetStartCreationVectorForFence(sideType);
            int endX = _configWrapper.GetEndXValueForFenceLane(creationVector.x);
            
            for (int x = creationVector.x; x < endX; x++)
            {
                var position = new Vector3(x, creationVector.y, creationVector.z);
                CreateFence(sideType, position, parent);
            }
        }

        private void GenerateDecorLane(SideType sideType, Transform parent)
        {
            Vector3Int creationVector = _configWrapper.GetStartCreationVectorForDecor(sideType);
            int endZ = _configWrapper.GetEndZValueForDecorLane();
            
            for (int z = creationVector.z; z < endZ; z++)
            {
                if (ShouldSpawnDecor())
                {
                    var position = new Vector3(creationVector.x, creationVector.y, z);
                    CreateRequiredCube(CubeType.Decor, position, parent);
                }
            }
        }

        private bool ShouldSpawnDecor()
        {
            int randomChanceForDecor = Random.Range(0, 101);
            return randomChanceForDecor < RandomChanceForDecor;
        }

        private Vector3 CalculateCubePosition(int x, int y, int z) =>
            new Vector3(x, y, z) + _configWrapper.GetStartPosition();
        
        private void CreateRequiredCube(CubeType cubeType, Vector3 at, Transform under)
        {
            string prefabPath = _genData.GetCubePrefabPath(cubeType);
            _battlefieldFactory.CreateBattlefieldItem(prefabPath, at, under);
        }

        private void CreateFence(SideType sideType, Vector3 at, Transform under)
        {
            string prefabPath = _genData.GetFencePrefabPath(sideType);
            _battlefieldFactory.CreateBattlefieldItem(prefabPath, at, under);
        }
    }
}