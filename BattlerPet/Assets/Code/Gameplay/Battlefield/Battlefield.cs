using Code.Data.Battlefield;
using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;
using Code.Data.Gameplay.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class Battlefield : IInitializable
{
    private readonly IBattlefieldFactory _battlefieldFactory;
    private readonly IStaticDataService _staticDataService;
    private BattlefieldGenData _genData;
    private BattlefieldConfig _battlefieldConfig;
    
    public Battlefield(IBattlefieldFactory battlefieldFactory, IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
        _battlefieldFactory = battlefieldFactory;
    }

    public void Initialize()
    {
        GetRequiredData();
        SetSkyboxMaterial();
        Transform parentForCubes = CreateParentForCubes();
        
        int botLayerStartY = GetYBotPoint();
        int topLayerStartY = GetYStartPointForTopLayer();
        int topLayerEndY = GetYFinishPointForTopLayer();
        
        GenerateLayer(botLayerStartY, topLayerStartY, parentForCubes);
        GenerateLayer(topLayerStartY, topLayerEndY, parentForCubes);
    }

    private void GetRequiredData()
    {
        _genData = _staticDataService.GetBattlefieldGenData();
        _battlefieldConfig = _genData.BattlefieldConfig;
    }

    private void SetSkyboxMaterial() =>
        RenderSettings.skybox = _genData.SkyBoxMaterial;

    private Transform CreateParentForCubes() => 
        new GameObject(nameof(Battlefield)).transform;

    private int GetYBotPoint() =>
        Mathf.FloorToInt(_battlefieldConfig.StartPositionOffset.y);

    private int GetYStartPointForTopLayer() => 
        _battlefieldConfig.GridSize.y - 1;

    private int GetYFinishPointForTopLayer() => 
        _battlefieldConfig.GridSize.y;

    private void GenerateLayer(int startY, int endY, Transform parent)
    {
        for (int y = startY; y < endY; y++)
            GenerateLayerForY(y, parent);
    }

    private void GenerateLayerForY(int y, Transform parent)
    {
        for (int x = 0; x <  _battlefieldConfig.GridSize.x; x++)
        {
            for (int z = 0; z <  _battlefieldConfig.GridSize.z; z++)
            {
                Vector3 position = CalculateCubePosition(x, y, z);
                CreatePrefab(y == GetYStartPointForTopLayer(), position, parent);
            }
        }
    }

    private Vector3 CalculateCubePosition(int x, int y, int z) =>
        new Vector3(x, y, z) + _battlefieldConfig.StartPositionOffset;

    private void CreatePrefab(bool isTopLayer, Vector3 position, Transform under)
    {
        string prefabPath = isTopLayer
            ? _genData.GetCubePath(CubeType.TopLayer)
            : _genData.GetCubePath(CubeType.BotLayer);

        _battlefieldFactory.CreateBattlefieldCube(prefabPath, position, under);
    }
}
}