using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}