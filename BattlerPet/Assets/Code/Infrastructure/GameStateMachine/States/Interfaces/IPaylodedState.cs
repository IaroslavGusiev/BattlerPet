namespace Code.Infrastructure.GameStateMachine
{
    public interface IPaylodedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}