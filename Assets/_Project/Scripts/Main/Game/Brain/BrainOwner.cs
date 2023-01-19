using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using SceneContext = _Project.Scripts.Main.Installers.SceneContext;

namespace _Project.Scripts.Main.Game.Brain
{
    public class BrainOwner : MonoBehaviour
    {
        [SerializeField] private Brain _brain;
        [SerializeField] private HealthBase _target;
        [SerializeField] private TransformInfo _transformInfoTarget;

        private bool _isTargetExist;
        private NavMeshAgent _navMeshAgent;

        public PlayerBase Player => SceneContext.Instance.Player;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public HealthBase Target => _target;
        public bool IsTargetExist => _isTargetExist;

        private void OnEnable()
        {
            SceneContext.Instance.BrainControl.AddBrain(this);
        }

        private void OnDisable()
        {
            SceneContext.Instance.BrainControl.RemoveBrain(this);
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Think()
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