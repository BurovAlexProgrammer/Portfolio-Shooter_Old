using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Installers;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Main.Game.Brain
{
    public class BrainOwner : MonoBehaviour
    {
        [SerializeField] private Brain _brain;
        [SerializeField] private HealthBase _target;
        [SerializeField] private TransformInfo _transformInfoTarget;

        private bool _isTargetExist;
        private NavMeshAgent _navMeshAgent;
        [Inject] PlayerBase _player;

        public PlayerBase Player => _player;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public HealthBase Target => _target;
        public bool IsTargetExist => _isTargetExist;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _brain.Think(this);
        }

        public void SetTargetHealth(HealthBase targetHealth)
        {
            _target = targetHealth;
            _isTargetExist = targetHealth != null;
        }
    }
}