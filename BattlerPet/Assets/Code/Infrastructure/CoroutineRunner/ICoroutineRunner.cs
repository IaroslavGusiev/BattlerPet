using System.Collections;

namespace Code.Infrastructure
{
    public interface ICoroutineRunner
    {
        void RunCoroutine(IEnumerator coroutine);
        void StopRunningCoroutine(IEnumerator coroutine);
    }
}