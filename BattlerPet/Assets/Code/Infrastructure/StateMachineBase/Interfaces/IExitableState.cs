using System.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public interface IExitableState
    {
        UniTask Exit(CancellationToken token);
    }
}