using Code.Data;
using Code.StaticData.Hero;

namespace Code.Services
{
    public interface IStaticDataService
    {
        HeroData HeroDataFor(HeroType heroType);
    }
}