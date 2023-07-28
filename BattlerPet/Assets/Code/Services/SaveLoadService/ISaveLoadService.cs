using Code.Data;
using Cysharp.Threading.Tasks;

namespace Code.Services
{
    public interface ISaveLoadService
    {
        UniTaskVoid SaveProgress();
        UniTask<PlayerProgress> LoadProgress();
    }
}