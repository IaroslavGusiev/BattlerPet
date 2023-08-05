using System;
using System.Linq;
using VContainer;
using Code.Services;
using VContainer.Unity;
using Code.Gameplay.Hero;
using Code.Data.Gameplay;
using CodeBase.Extensions;
using Code.StaticData.Hero;

namespace Code.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(IObjectResolver objectResolver, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _staticDataService = staticDataService;
        }

        public HeroBehaviour CreateHero(HeroType heroType)
        {
            HeroData data = _staticDataService.HeroDataFor(heroType);
            var prefab = _assetProvider.Get<HeroBehaviour>(data.PrefabPath);
            
            HeroBehaviour hero = _objectResolver.Instantiate(prefab)
                .With(hero => hero.HeroType = data.HeroType)
                .With(hero => hero.Id = new Guid().ToString());

            hero.InitializeWithState(new HeroState()
                .With(x => x.MaxHp = data.MaxHp)
                .With(x => x.CurrentHp = data.MaxHp)
                .With(x => x.MaxHaste = data.MaxHaste)
                .With(x => x.CurrentHaste = 0)
                .With(x => x.SkillStates = data.SkillData.Select(SkillState.FromSkillData).ToList()));

            return hero;
        }
    }
}