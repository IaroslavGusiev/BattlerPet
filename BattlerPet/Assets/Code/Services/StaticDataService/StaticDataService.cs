using Code.Data;
using System.Linq;
using Code.Data.Gameplay;
using Code.StaticData.Hero;
using System.Collections.Generic;

namespace Code.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;

        private Dictionary<HeroType, HeroData> _heroData; 

        public StaticDataService(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void LoadData()
        {
            LoadHeroData();
        }

        public HeroData HeroDataFor(HeroType heroType) => 
            _heroData[heroType]; 

        private void LoadHeroData()
        {
            _heroData = _assetProvider
                .GetAll<HeroData>(StaticDataPaths.HeroDataPath)
                .ToDictionary(x => x.HeroType, x => x);
        }
    }
}