using System;
using Code.Data;
using UnityEngine;
using CodeBase.Extensions;
using Code.Data.Battlefield;
using Code.Gameplay.Battlefield;
using System.Collections.Generic;
using Code.Data.Gameplay.Battlefield;

namespace Code.Services
{
    public class BattlefieldDataLoader
    {
        private readonly IAssetProvider _assetProvider;

        private readonly Dictionary<CubeType, List<CubeData>> _cubeData = new();
        private readonly Dictionary<TopLayerCubeType, List<CubeData>> _topLayersCubeData = new();
        private SkyboxData _skyboxData;
        private BattlefieldConfig _battlefieldConfig;

        public BattlefieldDataLoader(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void LoadData()
        {
            LoadBattlefieldConfig();
            LoadSkyBoxData();
            LoadCubeData();
        }
        
        public BattlefieldGenData PrepareBattlefieldGenData()
        {
            return new BattlefieldGenData()
                .With(x => x.BattlefieldConfig = _battlefieldConfig)
                .With(x => x.SkyBoxMaterial = GetRandomSkyBoxMaterial())
                .With(x => x.CubeData = PrepareCubeData());
        }

        private void LoadBattlefieldConfig() =>
            _battlefieldConfig = _assetProvider.Get<BattlefieldConfig>(StaticDataPath.BattlefieldConfigPath);

        private void LoadSkyBoxData() =>
            _skyboxData = _assetProvider.Get<SkyboxData>(StaticDataPath.SkyboxData);

        private void LoadCubeData()
        {
            foreach (CubeData data in _assetProvider.GetAll<CubeData>(StaticDataPath.CubeDataPath))
            {
                switch (data.CubeType)
                {
                    case CubeType.TopLayer:
                        HandleTopLayerCubeData(data);
                        break;
                    
                    case CubeType.BotLayer:
                    case CubeType.Decor:
                    case CubeType.Fence:
                        HandleOtherCubeData(data);
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException($"{data.CubeType} can't be process");
                }
            }
        }

        private void HandleTopLayerCubeData(CubeData data)
        {
            if (_topLayersCubeData.TryGetValue(data.TopLayerCubeType, out List<CubeData> topLayerList))
                topLayerList.Add(data);
            else
                _topLayersCubeData[data.TopLayerCubeType] = new List<CubeData> {data};
        }

        private void HandleOtherCubeData(CubeData data)
        {
            if (_cubeData.TryGetValue(data.CubeType, out List<CubeData> dataList))
                dataList.Add(data);
            else
                _cubeData[data.CubeType] = new List<CubeData> {data};
        }

        private Dictionary<CubeType, List<CubeData>> PrepareCubeData()
        {
            var data = new Dictionary<CubeType, List<CubeData>>
            {
                [CubeType.BotLayer] = new() {_cubeData[CubeType.BotLayer].PickRandom()},
                [CubeType.TopLayer] = _topLayersCubeData.Values.PickRandom(),
                [CubeType.Fence] = _cubeData[CubeType.Fence],
                [CubeType.Decor] = _cubeData[CubeType.Decor]
            };
            return data;
        }

        private Material GetRandomSkyBoxMaterial() => 
            _skyboxData.SkyBoxMaterials.PickRandom();
    }
}