using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game.Brain;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using static _Project.Scripts.Extension.Common;

namespace _Project.Scripts.Main.Game
{
    public class Character : MonoPoolItemBase
    {
        [SerializeField] private CharacterData _data;
        [SerializeField] private BrainOwner _brainOwner;
        [SerializeField] private HealthBase _health;
        [SerializeField] private Attacker _attacker;
        [SerializeField] private Animator _animator;
        [Header("Audio")] 
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioEvent _attackEvent;

        private NavMeshAgent _navMeshAgent;

        public CharacterData Data => _data;
        public Attacker Attacker => _attacker;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _brainOwner = GetComponent<BrainOwner>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<HealthBase>();
            _attacker = GetComponent<Attacker>();
            _audioSource = GetComponent<AudioSource>();

            var t = AnimatorParameters;
            
            if (_navMeshAgent != null)
            {
                _navMeshAgent.acceleration = _data.Acceleration;
                _navMeshAgent.speed = _data.Speed;
                _navMeshAgent.stoppingDistance = _data.MeleeRange - 0.1f;
            }

            if (_health != null)
            {
                _health.Init(_data.Health, _data.Health);
            }

            if (_attacker != null)
            {
                _attacker.Init(this);
                _attacker.DamageTargetAction += OnDamageTarget;
                _attacker.PlayAttackSoundAction += OnPlayAttackSound;
            }

            if (_animator != null)
            {
                _animator.ValidateParameters();
            }
        }

        private void OnDestroy()
        {
            _attacker.DamageTargetAction -= OnDamageTarget;
            _attacker.PlayAttackSoundAction -= OnPlayAttackSound;
        }

        public void SetTarget(GameObject target)
        {
            _brainOwner.SetTarget(target);
        }

        public void PlayAttack(GameObject target)
        {
            _animator.SetTrigger(AnimatorParameterNames.Attack);
        }

        private void OnDamageTarget()
        {
            _brainOwner.TargetHealth.TakeDamage(_data.MeleeDamage);
        }

        private void OnPlayAttackSound()
        {
            _attackEvent.Play(_audioSource);
        }
    }
}