using Code.Data.Gameplay;
using Code.Data.Gameplay.Battlefield;
using Code.StaticData.Hero;
using Code.Gameplay.Battlefield;

namespace Code.Services
{
    public interface IStaticDataService
    {
        HeroData HeroDataFor(HeroType heroType);
        BattlefieldConfig GetBattlefieldConfig();
    }
}