using Cysharp.Threading.Tasks;
using Code.UI.ScreenServiceSpace;

namespace Code.Services
{
    public interface IScreenService
    {
        UniTask<TScreen> ShowScreen<TScreen>() where TScreen : BaseScreen;
        UniTask<TScreen> ShowScreen<TScreen, TArg>(TArg arg) where TScreen : BaseScreen<TArg>;
    }
}