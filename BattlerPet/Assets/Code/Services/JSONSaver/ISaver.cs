using Cysharp.Threading.Tasks;

namespace Code.Services.JSONSaver
{
    public interface ISaver
    {
        UniTask SaveData<T>(string relativePath, T data);
        UniTask<T> LoadData<T>(string relativePath);
    }
}