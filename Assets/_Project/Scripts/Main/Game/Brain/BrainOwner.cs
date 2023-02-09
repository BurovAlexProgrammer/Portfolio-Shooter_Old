using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.Installers;
using UnityEngine;
using UnityEngine.AI;
using static _Project.Scripts.Main.AppServices.Services;

namespace _Project.Scripts.Main.Game.Brain
{
    public class BrainOwner : MonoBeh
    {
        [SerializeField] private Brain _brain;
        [SerializeField] private GameObject _target;
        [SerializeField] private HealthBase _targetHealth;
        [SerializeField] private TransformInfo _transformInfoTarget;
        [SerializeField, ReadOnlyField] private Character _character;
        [SerializeField, ReadOnlyField] NavMeshAgent _navMeshAgent;

        private bool _isTargetExist;

        public PlayerBase Player => GameContext.Instance.Player;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public GameObject Target => _target;
        public HealthBase TargetHealth => _targetHealth;
        public bool IsTargetExist => _isTargetExist;
        public Character Character => _character;

        private void OnEnable()
        {
            BrainControl.AddBrain(this);
        }

        private void OnDisable()
        {
            BrainControl.RemoveBrain(this);
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _character = GetComponent<Character>();
        }

        public void Think()
        {
            _brain.Think(this);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            _targetHealth = target.GetComponent<HealthBase>();
            _isTargetExist = _target != null;
        }
    }
}