using System.Collections.Generic;
using UnityEngine;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class EntityBehaviour : MonoBehaviour, IEntity
    {
        public string Id { get; private set; }
        [field: SerializeField] public EntityType EntityType { get; private set; } 
        
        [SerializeField] private EntityAnimator _entityAnimator;
        [SerializeField] private GameObject _turnIndicator;
        [SerializeField] private ActorUI _actorUI;
        
        private EntityController _entityController;
        
        public void Initialize(EntityController entityController, string uniqueId)
        {
            Id = uniqueId;
            _entityController = entityController;
            _actorUI.Construct(entityController.ReactiveModel);
        }

        public IEnumerable<ISkillModel> GetReadySkills()
        {
            return _entityController.GetReadySkills();
        }

        public void TakeDamage(float incomeDamage)
        {
            _entityController.TakeDamage(incomeDamage);
            _entityAnimator.PlayTakeDamage();
        }

        public bool IsReadyForNextTick(float hasteTickValue) => 
            _entityController.IsReadyForNextTick(hasteTickValue);

        public bool IsReadyForTurn() => 
            _entityController.IsReadyForTurn();

        public void TickSkillsCooldown(float deltaTime) => 
            _entityController.TickSkillsCooldown(deltaTime);

        public void EnableTurnIndicator(bool enable) =>
            _turnIndicator.SetActive(enable);

        public void AnimateDeath() => 
            _entityAnimator.PlayDeath();

        public void ReplenishHaste(float amountToAdd) =>
            _entityController.UpdateHaste(amountToAdd);
    }
}