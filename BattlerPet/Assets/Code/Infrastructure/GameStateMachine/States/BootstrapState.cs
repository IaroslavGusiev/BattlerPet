using UnityEngine;

namespace Code.Infrastructure.GameStateMachine
{
    public class BootstrapState : IState
    {
        public void Enter()
        {
            Debug.Log("<color=green>Enter BootstrapState</color>");
        }

        public void Exit()
        {
            
        }
    }
}