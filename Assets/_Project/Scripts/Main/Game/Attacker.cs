using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main.Game
{
    [DisallowMultipleComponent]
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private float _attackTimer;

        private bool _attackInProgress;
        public event Action DamageTargetAction;
        public event Action PlayAttackSoundAction;

        public bool ReadyToAttack => !_attackInProgress && _attackTimer <= 0f;

        public void Init(CharacterController characterController)
        {
            _characterData = characterController.Data;
        }

        public void StartAttack()
        {
            _attackInProgress = true;
        }

        public void EndAttack()
        {
            _attackInProgress = false;
        }

        public async UniTask RunAttackDelay(CancellationToken cancellationToken = default)
        {
            _attackTimer = _characterData.AttackDelay;

            while (_attackTimer > 0f)
            {
                await UniTask.NextFrame(cancellationToken);
                _attackTimer -= Time.deltaTime;
            }
        }
        
        //Animation Event
        public void DamageTarget()
        {
            if (this == null) return;
            
            DamageTargetAction?.Invoke();
        }
        
        //Animation Event
        public void PlayAttackSound()
        {
            if (this == null) return;
            
            PlayAttackSoundAction?.Invoke();
        }
    }
}