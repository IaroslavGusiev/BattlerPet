using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
}