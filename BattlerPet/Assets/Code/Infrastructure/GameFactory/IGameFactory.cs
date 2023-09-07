using Code.Data.Gameplay;
using Code.Gameplay.Battlefield;
using Code.Gameplay.Hero;

namespace Code.Infrastructure
{
    public interface IGameFactory
    {
        HeroBehaviour CreateHero(HeroType heroType);
    }
}