using Code.Gameplay.Entity;

namespace Code.Gameplay.Core
{
    public class SkillCooldownService
    {
        private readonly IEntityRegister _entityRegister;

        public SkillCooldownService(IEntityRegister entityRegister) => 
            _entityRegister = entityRegister;

        public void CooldownTick(float deltaTime)
        {
            foreach (IEntity entity in _entityRegister.AllActiveEntities())
                entity.TickSkillsCooldown(deltaTime);
        }
    }
}