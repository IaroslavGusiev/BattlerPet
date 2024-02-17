using Code.Data;
using Cysharp.Threading.Tasks;

namespace Code.Services
{
    public interface ISaveLoadService
    {
        UniTask SaveProgress();
        UniTask<PlayerProgress> LoadProgress();
    }
}