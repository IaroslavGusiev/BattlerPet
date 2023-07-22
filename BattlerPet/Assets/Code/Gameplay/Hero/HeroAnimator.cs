using Code.Data;
using UnityEngine;
using CodeBase.Extensions;

namespace Code.Gameplay.Hero
{
    public class HeroAnimator : MonoBehaviour
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
        private int[] _attacksHashes;
        private int[] _otherAnimBoolHashes;
 
        private void Awake()
        {
            _attacksHashes = new[] { _basicAttackHash, _secondAttackHash, _ultimateHash };
            _otherAnimBoolHashes = new[] { _victoryHash, _runHash, _sadHash };
        }

        public void PlayAttack(AttackType attackType)
        {
            ResetAllTrigger();
            _animator.SetTrigger(_attacksHashes[attackType.ToInt()]);
        }

        public void PlaySad(bool start)
        {
            ResetAllTrigger();
            _animator.SetBool(_sadHash, start);
        }

        public void PlayVictory(bool start)
        {
            ResetAllTrigger();
            _animator.SetBool(_victoryHash, start);
        }

        public void Run(bool start)
        {
            ResetAllTrigger();
            _animator.SetBool(_runHash, start);
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

        private void ResetAllTrigger()
        {
            if (_animator.runtimeAnimatorController == null)
                return;

            foreach (int hash in _attacksHashes)
                _animator.ResetTrigger(hash);

            foreach (int hash in _otherAnimBoolHashes)
                _animator.SetBool(hash, false);
        }
    }
}