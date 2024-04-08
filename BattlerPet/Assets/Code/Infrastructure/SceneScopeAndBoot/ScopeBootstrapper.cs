using System.Threading;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public abstract class ScopeBootstrapper<T> : IAsyncStartable where T : StateMachine
    {
        protected readonly T StateMachine;
        protected readonly StateFactory StateFactory;

        protected ScopeBootstrapper(T stateMachine, StateFactory stateFactory)
        {
            StateMachine = stateMachine;
            StateFactory = stateFactory;
        }

        public async UniTask StartAsync(CancellationToken cancellation) => 
            await StartLogic(cancellation);

        protected abstract UniTask StartLogic(CancellationToken token);
    }
}
        