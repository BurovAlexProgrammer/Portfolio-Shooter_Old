using _Project.Data.Game;
using _Project.Scripts.Extension;
using UnityEngine;
using UnityEngine.AI;
using SceneContext = _Project.Scripts.Main.Installers.SceneContext;

namespace _Project.Scripts.Main.Game.Brain
{
    public class BrainOwner : MonoBeh
    {
        [SerializeField] private Brain _brain;
        [SerializeField] private GameObject _target;
        [SerializeField] private HealthBase _targetHealth;
        [SerializeField] private TransformInfo _transformInfoTarget;
        [SerializeField] private Character _character;

        private bool _isTargetExist;
        private NavMeshAgent _navMeshAgent;

        public PlayerBase Player => SceneContext.Instance.Player;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public GameObject Target => _target;
        public HealthBase TargetHealth => _targetHealth;
        public bool IsTargetExist => _isTargetExist;
        public Character Character => _character;

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
            _isTargetExist = _targetHealth != null;
        }
    }
}