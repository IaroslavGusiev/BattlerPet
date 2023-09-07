using System;

namespace Code.Infrastructure.GameStateMachine
{
    public interface IStateFactory 
    {
        Type StateType { get; }
        IExitableState Create(IGameStateMachine gameStateMachine);
    }
}