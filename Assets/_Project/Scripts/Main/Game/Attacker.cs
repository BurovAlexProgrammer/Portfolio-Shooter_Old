using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private float _attackTimer;

        private bool _inProgress;
        public Action DamageTargetAction;
        public Action PlayAttackSoundAction;

        public bool ReadyToAttack => !_inProgress && _attackTimer <= 0f;

        public void Init(Character character)
        {
            _characterData = character.Data;
        }

        public void StartAttack()
        {
            _inProgress = true;
        }

        public void EndAttack()
        {
            _inProgress = false;
        }

        public async void RunAttackDelay()
        {
            _attackTimer = _characterData.AttackDelay;

            while (_attackTimer > 0f)
            {
                await UniTask.NextFrame();
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