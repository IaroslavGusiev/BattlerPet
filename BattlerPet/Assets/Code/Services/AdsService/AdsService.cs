using System;
using UnityEngine;

namespace Code.Services
{
    public class AdsService : IAdsService
    {
        public event Action RewardedVideoReady;
        
        public bool IsRewardedVideoReady { get; }
        
        public void Initialize()
        {
            Debug.Log("Initialization of ads service isn't implemented yet");
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Debug.Log("Showing of ads isn't implemented yet");
        }
    }
}