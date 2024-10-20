using UnityEngine;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class EntityBehaviour : MonoBehaviour 
    {
        [field: SerializeField] public EntityType EntityType { get; private set; }

        [SerializeField] private EntityAnimator _entityAnimator;
        [SerializeField] private GameObject _turnIndicator;
        [SerializeField] private ActorUI _actorUI;
        
        public EntityBehaviour Initialize(IReactiveModel reactiveModel)
        {
            _actorUI.Construct(reactiveModel);
            return this;
        }

        public void EnableTurnIndicator(bool enable) =>
            _turnIndicator.SetActive(enable);

        public void AnimateDeath() => 
            _entityAnimator.PlayDeath();
        
        public void AnimateSKill(AttackType attackType) => 
            _entityAnimator.PlayAttack(attackType);
    }
}