using Code.Data;
using Code.Gameplay.Hero;

namespace Code.Infrastructure.GameFactory
{
    public interface IGameFactory
    {
        HeroBehaviour CreateHero(HeroType heroType);
    }
}