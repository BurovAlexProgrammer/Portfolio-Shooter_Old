using System;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Game.Brain;
using _Project.Scripts.Main.Wrappers;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Data.Game
{
    public class Character : MonoPoolItemBase
    {
        [SerializeField] private CharacterData _data;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private BrainOwner _brainOwner;

        private NavMeshAgent _navMeshAgent;
        
        public CharacterData Data => _data;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _brainOwner = GetComponent<BrainOwner>();
            
            if (_navMeshAgent != null)
            {
                _navMeshAgent.speed = _data.Speed;
                _navMeshAgent.stoppingDistance = _data.MeleeRange - 0.1f;
            }
        }

        public void SetTarget(GameObject target)
        {
            _brainOwner.SetTarget(target);
        }

        public void PlayAttack(GameObject target)
        {
           // AnimatorController.
        }
    }
}
