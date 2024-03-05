using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public interface ISkillHandler
    {
        void TickSkillsCooldown(float deltaTime);
        IEnumerable<ISkillModel> GetReadySkills();
    }
}