using System;

namespace Code.Gameplay.Core
{
    public interface IBattleTurnService
    {
        event Action<EntityAction> OnEntitySkillExecution;
        
        void StartBattle();
        void EndBattle();
    }
}