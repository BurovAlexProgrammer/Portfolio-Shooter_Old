using System.Threading;
using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Audio;
using _Project.Scripts.Main.Game.Brain;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using static _Project.Scripts.Extension.Common;

namespace _Project.Scripts.Main.Game
{
    public class CharacterController : BasePoolItem
    {
        [SerializeField] private CharacterData _data;
        [SerializeField, ReadOnlyField] private BrainOwner _brainOwner;
        [SerializeField, ReadOnlyField] private HealthBase _health;
        [SerializeField, ReadOnlyField] private Attacker _attacker;
        [SerializeField, ReadOnlyField] private Animator _animator;

        [Header("Audio")] 
        [SerializeField, ReadOnlyField] private AudioSource _audioSource;
        [SerializeField] private AudioEvent _attackEvent;

        [Inject] private StatisticService _statisticService;
        [Inject] private EventListenerService _eventListener;
        
        public CancellationToken CancellationToken { get; private set; }
        private NavMeshAgent _navMeshAgent;

        public CharacterData Data => _data;
        public Attacker Attacker => _attacker;
        public HealthBase Health => _health;

        private void Awake()
        {
            CancellationToken = gameObject.GetCancellationTokenOnDestroy();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _brainOwner = GetComponent<BrainOwner>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<HealthBase>();
            _attacker = GetComponent<Attacker>();
            _audioSource = GetComponent<AudioSource>();
            _eventListener.SubscribeCharacter(this);

            if (_navMeshAgent != null)
            {
                _navMeshAgent.acceleration = _data.Acceleration;
                _navMeshAgent.speed = _data.Speed;
                _navMeshAgent.stoppingDistance = _data.MeleeRange - 0.1f;
            }

            if (Health != null)
            {
                Health.Init(_data.Health, _data.Health);
                Health.OnDead += OnDead;
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

        private void OnDead()
        {
            if (_brainOwner != null && _attacker != null)
            {
                _statisticService.AddValueToRecord(StatisticData.RecordName.KillMonsterCount, 1);
            }
        }

        private void OnDestroy()
        {
            _attacker.DamageTargetAction -= OnDamageTarget;
            _attacker.PlayAttackSoundAction -= OnPlayAttackSound;
        }

        public async UniTask PlayAttack()
        {
            _animator.SetTrigger(AnimatorParameterNames.Attack);
            await _animator.GetClipLength(0).WaitInSeconds(CancellationToken);
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