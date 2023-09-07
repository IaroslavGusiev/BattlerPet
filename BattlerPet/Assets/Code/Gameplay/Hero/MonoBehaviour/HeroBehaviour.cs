using UnityEngine;
using Code.Data.Gameplay;

namespace Code.Gameplay.Hero
{
    public class HeroBehaviour : MonoBehaviour
    {
        public HeroType HeroType { get; set; }
        public string Id { get; set; }
        
        [SerializeField] private HeroAnimator _heroAnimator;
        private HeroModel _heroModel;

        public void InitializeHeroModel(HeroModel heroModel) => 
            _heroModel = heroModel;
    }
}