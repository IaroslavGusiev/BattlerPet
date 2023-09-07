using System;
using VContainer;
using System.Linq;
using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Gameplay.Hero;
using Code.Data.Gameplay;
using CodeBase.Extensions;
using Code.StaticData.Hero;
using Code.Gameplay.Battlefield;

namespace Code.Infrastructure
{
    public class GameFactory : IGameFactory, IBattlefieldFactory
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
            var prefab = GetPrefab<HeroBehaviour>(data.PrefabPath);
            
            HeroBehaviour hero = _objectResolver.Instantiate(prefab)
                .With(hero => hero.HeroType = data.HeroType)
                .With(hero => hero.Id = new Guid().ToString());

            hero.InitializeHeroModel(new HeroModel() // TODO: model factory, UniRX for stats 
                .With(x => x.MaxHp = data.MaxHp)
                .With(x => x.CurrentHp = data.MaxHp)
                .With(x => x.MaxHaste = data.MaxHaste)
                .With(x => x.CurrentHaste = 0)
                .With(x => x.SkillModels = data.SkillData.Select(skillData => new SkillModel(skillData)).ToList()));
            
            return hero;
        }

        public BattlefieldBehaviour CreateBattlefieldBehaviour(string path)
        {
            var prefab = GetPrefab<BattlefieldBehaviour>(path);
            return _objectResolver.Instantiate(prefab, null);
        }

        public Cube CreateBattlefieldItem(string path, Vector3 at, Transform under) // TODO: Separate to BattlefieldFactory
        {
            var prefab = GetPrefab<Cube>(path);
            return _objectResolver.Instantiate(prefab, at, prefab.transform.rotation, under);
        }

        private T GetPrefab<T>(string path) where T : MonoBehaviour => 
            _assetProvider.Get<T>(path);
    }
}