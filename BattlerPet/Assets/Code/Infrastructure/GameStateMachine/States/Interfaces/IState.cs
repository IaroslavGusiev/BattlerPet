namespace Code.Infrastructure.GameStateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}