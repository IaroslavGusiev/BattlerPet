using System.Linq;
using System.Threading;
using Code.StaticData.UI;
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
        private List<ScreenServiceConfig> _screenServiceConfigs = new();

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask Initialize(CancellationToken token)
        {
            await LoadEntityData(token);
            await LoadBattlefieldConfig(token);
            await LoadScreenServiceConfig(token);
        }

        public BattlefieldConfig GetBattlefieldConfig() =>
            _battlefieldConfigs.FirstOrDefault();

        public EntityConfig EntityConfigFor(EntityType entityType) => 
            _heroData[entityType];

        public ScreenServiceConfig GetScreenServiceConfig() => 
            _screenServiceConfigs.FirstOrDefault();

        public SkillConfig SkillConfigFor(EntityType entityType, AttackType attackType)
        {
            List<SkillConfig> configs = EntityConfigFor(entityType).SkillConfigs;
            return configs.FirstOrDefault(x => x.AttackType == attackType);
        }

        private async UniTask LoadEntityData(CancellationToken token)
        {
            EntityConfig[] configs = await GetConfigs<EntityConfig>(token);
            _heroData = configs.ToDictionary(x => x.EntityType, x => x);
        }

        private async UniTask LoadBattlefieldConfig(CancellationToken token)
        {
            BattlefieldConfig[] configs = await GetConfigs<BattlefieldConfig>(token);
            _battlefieldConfigs = configs.ToList();
        }

        private async UniTask LoadScreenServiceConfig(CancellationToken token)
        {
            ScreenServiceConfig[] configs = await GetConfigs<ScreenServiceConfig>(token);
            _screenServiceConfigs = configs.ToList();
        } 

        private async UniTask<T[]> GetConfigs<T>(CancellationToken token) where T : class
        {
            List<string> keys = await GetConfigsKeys<T>(token);
            T[] loadedConfigs = await _assetProvider.LoadAll<T>(keys);
            return loadedConfigs;
        }

        private async UniTask<List<string>> GetConfigsKeys<T>(CancellationToken token) =>
            await _assetProvider.FetchAssetKeysByLabel<T>(AssetLabels.Configs, token);
    }
}