using System;

namespace Code.Infrastructure.Services
{
    public interface IAdsService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}