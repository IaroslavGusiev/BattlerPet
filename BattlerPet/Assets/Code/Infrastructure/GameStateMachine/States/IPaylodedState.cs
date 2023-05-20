namespace Code.Infrastructure.GameStateMachine
{
    public interface IPaylodedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}