using Code.Data;

namespace Code.Services
{
    public interface IPlayerProgressProvider
    {
        PlayerProgress Progress { get; set; }
    }
}