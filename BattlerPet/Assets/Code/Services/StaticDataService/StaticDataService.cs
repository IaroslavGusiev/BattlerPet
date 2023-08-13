using Code.Data;
using System.Linq;
using Code.Data.Gameplay;
using Code.StaticData.Hero;
using Code.Gameplay.Battlefield;
using System.Collections.Generic;

namespace Code.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly BattlefieldDataLoader _battlefieldDataLoader;
        
        private Dictionary<HeroType, HeroData> _heroData;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _battlefieldDataLoader = new BattlefieldDataLoader(_assetProvider);
        }

        public void LoadData()
        {
            LoadHeroData();
            _battlefieldDataLoader.LoadData();
        }

        public HeroData HeroDataFor(HeroType heroType) =>
            _heroData[heroType];

        public BattlefieldGenData GetBattlefieldGenData() => 
            _battlefieldDataLoader.PrepareBattlefieldGenData();

        private void LoadHeroData()
        {
            _heroData = _assetProvider
                .GetAll<HeroData>(StaticDataPath.HeroDataPath)
                .ToDictionary(x => x.HeroType, x => x);
        }
    }
}