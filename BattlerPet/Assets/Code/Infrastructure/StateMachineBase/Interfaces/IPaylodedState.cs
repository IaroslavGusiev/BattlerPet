using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public interface IPaylodedState<in TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload, CancellationToken token);
    }
}