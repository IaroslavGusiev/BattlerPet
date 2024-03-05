using Code.Gameplay.Entity;

namespace Code.Gameplay.Core.AI
{
    public interface IArtificialIntelligence
    {
        EntityAction MakeBestDecision(IEntity entity);
    }
}