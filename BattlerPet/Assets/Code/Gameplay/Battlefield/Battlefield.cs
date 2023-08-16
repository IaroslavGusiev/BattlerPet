using UnityEngine;
using Code.Services;
using VContainer.Unity;
using Code.Infrastructure;
using CodeBase.Extensions;
using Code.Data.Gameplay.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class Battlefield : IInitializable
    {
        private readonly IBattlefieldFactory _battlefieldFactory;
        private readonly IStaticDataService _staticDataService;

        private BattlefieldGenerator _battlefieldGenerator;
        private BattlefieldGenData _genData;

        public Battlefield(IBattlefieldFactory battlefieldFactory, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _battlefieldFactory = battlefieldFactory;
        }

        public void Initialize()
        {
            GetData();
            CreateAmbianceParticles();
            CreateBattlefieldGenerator();
        }

        private void GetData()
        {
            _staticDataService.GetBattlefieldGenData()
                .With(x => _genData = x)
                .With(x => x.WarmUp())
                .With(x => SetSkyboxMaterial(x.SkyBoxMaterial));
        }

        private void CreateAmbianceParticles()
        {
            Transform container = new GameObject("AmbianceParticle").transform;
            BattlefieldAmbianceParticleData data = _genData.BattlefieldConfig.BattlefieldAmbianceParticleData;
            data.SpawnPositions.ForEach(x => _battlefieldFactory.CreateBattlefieldItem(data.ParticlePrefabPath, x, container));
        }

        private void CreateBattlefieldGenerator()
        {
            _battlefieldGenerator = new BattlefieldGenerator(_battlefieldFactory, _genData);
            _battlefieldGenerator.Generate();
        }

        private void SetSkyboxMaterial(Material skyboxMaterial) =>
            RenderSettings.skybox = skyboxMaterial;
    }
}