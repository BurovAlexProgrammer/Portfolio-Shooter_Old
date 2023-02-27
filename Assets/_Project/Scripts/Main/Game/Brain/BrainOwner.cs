using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.AppServices.SceneServices;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Game.Health;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Main.Game.Brain
{
    public class BrainOwner : MonoBehaviour
    {
        [SerializeField] private Brain _brain;
        [SerializeField] private GameObject _target;
        [SerializeField] private HealthBase _targetHealth;
        [SerializeField] private TransformInfo _transformInfoTarget;
        [SerializeField, ReadOnlyField] private CharacterController _characterController;
        [SerializeField, ReadOnlyField] NavMeshAgent _navMeshAgent;

        private BrainControlService _brainControl;

        private bool _isTargetExist;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public GameObject Target => _target;
        public HealthBase TargetHealth => _targetHealth;
        public bool IsTargetExist => _isTargetExist;
        public CharacterController CharacterController => _characterController;

        private void OnEnable()
        {
            _brainControl.AddBrain(this);
        }

        private void OnDisable()
        {
            _brainControl.RemoveBrain(this);
        }

        private void Awake()
        {
            _brainControl = GamePlayContext.BrainControlService;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _characterController = GetComponent<CharacterController>();
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