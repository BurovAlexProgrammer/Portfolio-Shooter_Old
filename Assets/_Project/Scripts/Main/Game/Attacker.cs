using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private float _attackTimer;

        private bool _attackInProgress;
        public event Action DamageTargetAction;
        public event Action PlayAttackSoundAction;

        public bool ReadyToAttack => !_attackInProgress && _attackTimer <= 0f;

        public void Init(Character character)
        {
            _characterData = character.Data;
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
            DamageTargetAction?.Invoke();
        }
        
        //Animation Event
        public void PlayAttackSound()
        {
            PlayAttackSoundAction?.Invoke();
        }
    }
}