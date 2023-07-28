﻿using System;

namespace Code.Services
{
    public interface IAdsService
    {
        event Action RewardedVideoReady;
        
        bool IsRewardedVideoReady { get; }
        void ShowRewardedVideo(Action onVideoFinished);
    }
}