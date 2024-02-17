using System.Linq;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using Code.StaticData.Gameplay;
using Code.Gameplay.Battlefield;
using System.Collections.Generic;

namespace Code.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;

        private Dictionary<EntityType, EntityConfig> _heroData = new();
        private List<BattlefieldConfig> _battlefieldConfigs = new();

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask Initialize()
        {
            await LoadEntityData();
            await LoadBattlefieldConfig();
        }

        public EntityConfig GetEntityData(EntityType entityType) => 
            _heroData[entityType];

        public BattlefieldConfig GetBattlefieldConfig() =>
            _battlefieldConfigs.FirstOrDefault();

        private async UniTask LoadEntityData()
        {
            EntityConfig[] configs = await GetConfigs<EntityConfig>();
            _heroData = configs.ToDictionary(x => x.EntityType, x => x);
        }

        private async UniTask LoadBattlefieldConfig()
        {
            BattlefieldConfig[] configs = await GetConfigs<BattlefieldConfig>();
            _battlefieldConfigs = configs.ToList();
        }

        private async UniTask<T[]> GetConfigs<T>() where T : class
        {
            List<string> keys = await GetConfigsKeys<T>();
            T[] loadedConfigs = await _assetProvider.LoadAll<T>(keys);
            return loadedConfigs;
        }

        private async UniTask<List<string>> GetConfigsKeys<T>() =>
            await _assetProvider.FetchAssetKeysByLabel<T>(AssetLabels.Configs);
    }
}