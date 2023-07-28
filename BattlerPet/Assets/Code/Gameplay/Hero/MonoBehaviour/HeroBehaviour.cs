using Code.Data;
using UnityEngine;

namespace Code.Gameplay.Hero
{
    public class HeroBehaviour : MonoBehaviour
    {
        public HeroType HeroType { get; set; }
        public string Id { get; set; }
        
        [SerializeField] private HeroAnimator _heroAnimator;
        private HeroState _heroState;

        public void InitializeWithState(HeroState heroState)
        {
            _heroState = heroState;
        }
    }
}