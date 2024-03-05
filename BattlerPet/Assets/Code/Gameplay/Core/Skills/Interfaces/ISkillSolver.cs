namespace Code.Gameplay.Core
{
    public interface ISkillSolver
    {
        void ProcessEntityAction(EntityAction entityAction);
        void SkillDelaysTick(float deltaTime);
    }
}