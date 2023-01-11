using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.AI;
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
        [Inject] private BrainControlService _brainControlService;

        public PlayerBase Player => _player;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public HealthBase Target => _target;
        public bool IsTargetExist => _isTargetExist;

        private void OnEnable()
        {
            _brainControlService.AddBrain(this);
        }

        private void OnDisable()
        {
            _brainControlService.RemoveBrain(this);
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Think()
        {
            Debug.Log("Think");
            _brain.Think(this);
        }

        public void SetTargetHealth(HealthBase targetHealth)
        {
            _target = targetHealth;
            _isTargetExist = targetHealth != null;
        }
    }
}