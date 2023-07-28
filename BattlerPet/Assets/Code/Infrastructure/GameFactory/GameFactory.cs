using System;
using Code.Data;
using VContainer;
using Code.Services;
using VContainer.Unity;
using Code.Gameplay.Hero;
using Code.StaticData.Hero;
using CodeBase.Extensions;

namespace Code.Infrastructure.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
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
            HeroBehaviour hero = _objectResolver.Instantiate(data.Prefab)
                .With(hero => hero.HeroType = data.HeroType)
                .With(hero => hero.Id = new Guid().ToString());

            hero.InitializeWithState(new HeroState()
                .With(x => x.MaxHp = data.MaxHp)
                .With(x => x.CurrentHp = data.MaxHp)
                .With(x => x.MaxHaste = data.MaxHaste)
                .With(x => x.CurrentHaste = 0));

            return hero;
        }
    }
}