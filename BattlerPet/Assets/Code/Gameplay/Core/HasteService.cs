using Code.Gameplay.Entity;

namespace Code.Gameplay.Core
{
    public class HasteService
    {
        private const int HasteTickValue = 5;
        private readonly IEntityRegister _entityRegister;

        public HasteService(IEntityRegister entityRegister) => 
            _entityRegister = entityRegister;

        public void IncreaseHasteTick()
        {
            foreach (IEntity entity in _entityRegister.AllActiveEntities())
            {
                entity.EnableTurnIndicator(false);
                entity.IncreaseHaste(HasteTickValue);
            }
        }

        public bool EntityIsReadyOnNextTick()
        {
            foreach (IEntity entity in _entityRegister.AllActiveEntities())
            {
                if (entity.IsReadyForNextTick(HasteTickValue))
                {
                    entity.EnableTurnIndicator(true);
                    return true;
                }
            }
            return false;
        }
    }
}