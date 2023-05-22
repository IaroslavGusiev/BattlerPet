using Code.Data;

namespace Code.Services
{
    public interface IProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}