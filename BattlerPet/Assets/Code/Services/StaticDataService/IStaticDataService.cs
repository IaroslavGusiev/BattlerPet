using Code.Data.Gameplay;
using Code.StaticData.Hero;
using Code.Gameplay.Battlefield;

namespace Code.Services
{
    public interface IStaticDataService
    {
        HeroData HeroDataFor(HeroType heroType);
        BattlefieldGenData GetBattlefieldGenData();
    }
}