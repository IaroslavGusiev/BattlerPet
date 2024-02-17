using UnityEngine;
using CodeBase.Extensions;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Entity
{
    public class EntityAnimator : MonoBehaviour
    {
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _sadHash = Animator.StringToHash("Sad");
        private readonly int _runHash = Animator.StringToHash("Run");
        private readonly int _victoryHash = Animator.StringToHash("Victory");
        private readonly int _takeDamageHash = Animator.StringToHash("TakeDamage");
        private readonly int _basicAttackHash = Animator.StringToHash("BasicAttack");
        private readonly int _ultimateHash = Animator.StringToHash("Ultimate Skill");
        private readonly int _secondAttackHash = Animator.StringToHash("Second Attack");

        [SerializeField] private Animator _animator;
        private int[] _attackHashes;
        private int[] _otherAnimBoolHashes;
 
        private void Awake()
        {
            _attackHashes = new[] { _basicAttackHash, _secondAttackHash, _ultimateHash };
            _otherAnimBoolHashes = new[] { _victoryHash, _runHash, _sadHash };
        }

        public void PlaySad(bool isSad) => 
            SetBoolParameter(_sadHash, isSad);

        public void PlayVictory(bool isVictorious) => 
            SetBoolParameter(_victoryHash, isVictorious);

        public void Run(bool isRunning) => 
            SetBoolParameter(_runHash, isRunning);

        public void PlayAttack(AttackType attackType)
        {
            ResetAllTrigger();
            _animator.SetTrigger(_attackHashes[attackType.ToInt()]);
        }

        public void PlayDeath()
        {
            ResetAllTrigger();
            _animator.SetTrigger(_dieHash);
        }
        
        public void PlayTakeDamage()
        {
            ResetAllTrigger();
            _animator.SetTrigger(_takeDamageHash);
        }
        
        private void SetBoolParameter(int hash, bool value)
        {
            ResetAllTrigger();
            _animator.SetBool(hash, value);
        }

        private void ResetAllTrigger()
        {
            if (_animator.runtimeAnimatorController == null)
                return;

            foreach (int hash in _attackHashes)
                _animator.ResetTrigger(hash);

            foreach (int hash in _otherAnimBoolHashes)
                _animator.SetBool(hash, false);
        }
    }
}