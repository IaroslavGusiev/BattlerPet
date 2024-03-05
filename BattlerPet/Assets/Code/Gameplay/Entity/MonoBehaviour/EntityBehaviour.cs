using UnityEngine;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

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

        public void IncreaseHealth(float value) => 
            _entityController.IncreaseHealth(value);

        public void IncreaseHaste(float value) => 
            _entityController.IncreaseHaste(value);

        public void ReduceHealth(float value)
        {
            _entityController.ReduceHealth(value);
            _entityAnimator.PlayTakeDamage();
        }

        public void ReduceHaste(float value) => 
            _entityController.ReduceHaste(value);

        public void ResetHasteToZero() => 
            _entityController.ResetHasteToZero();

        public IEnumerable<ISkillModel> GetReadySkills() => 
            _entityController.GetReadySkills();

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
        
        public void ExecuteSkill(AttackType attackType) // TODO: rename this enum. More mention and relation to skill not attack. 
        {
            _entityAnimator.PlayAttack(attackType);
            _entityController.PutSkillOnCooldown(attackType);
        }
    }
}