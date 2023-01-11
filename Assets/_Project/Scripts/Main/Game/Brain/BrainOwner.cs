using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Installers;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.Scripts.Main.Game.Brain
{
    public class BrainOwner : MonoBehaviour
    {
        [SerializeField] private Brain _brain;
        [SerializeField] private HealthBase _healthTarget;
        [SerializeField] private TransformInfo _transformInfoTarget;

        private NavMeshAgent _navMeshAgent;
        private bool _isHealthTargetExist;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _brain.Think(this);
            
            if (_isHealthTargetExist)
            {
                _navMeshAgent.SetDestination(_healthTarget.transform.position);
            }
            else
            {
                FindTarget();
            }
        }

        private void FindTarget()
        {
            var pl = GameObject.FindWithTag("Player");
            var h = pl.GetComponent<PlayerBase>();
            _healthTarget = pl.GetComponent<PlayerBase>().Health;
            _isHealthTargetExist = _healthTarget != null;
        }
    }
}