using System.Threading;
using Main.Extension.Attributes;
using Main.Audio;
using Main.Game.Brain;
using Main.Game.Health;
using Main.Wrappers;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;
using UnityEngine;
using UnityEngine.AI;
using static Main.Extension.Common;

namespace Main.Game
{
    [DisallowMultipleComponent]
    public class CharacterController : BasePoolItem
    {
        [SerializeField] private CharacterData _data;
        [SerializeField, ReadOnlyField] private BrainOwner _brainOwner;
        [SerializeField, ReadOnlyField] private HealthBase _health;
        [SerializeField, ReadOnlyField] private Attacker _attacker;
        [SerializeField, ReadOnlyField] private Animator _animator;
        [Header("Audio")] 
        [SerializeField, ReadOnlyField] private AudioSource _audioSource;
        [SerializeField] private AudioEvent _attackAudioEvent;

        private StatisticService _statisticService;

        public CancellationToken CancellationToken { get; private set; }
        private NavMeshAgent _navMeshAgent;

        public CharacterData Data => _data;
        public Attacker Attacker => _attacker;
        public HealthBase Health => _health;

        private void Awake()
        {
            _statisticService = Context.Resolve<StatisticService>();
            CancellationToken = gameObject.GetCancellationTokenOnDestroy();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _brainOwner = GetComponent<BrainOwner>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<HealthBase>();
            _attacker = GetComponent<Attacker>();
            _audioSource = GetComponent<AudioSource>();

            if (_navMeshAgent != null)
            {
                _navMeshAgent.acceleration = _data.Acceleration;
                _navMeshAgent.speed = _data.Speed;
                _navMeshAgent.stoppingDistance = _data.MeleeRange - 0.1f;
            }

            if (_health != null)
            {
                _health.Init(_data.Health, _data.Health);
                _health.OnDead += OnDead;
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
            if (_attacker != null)
            {
                _attacker.DamageTargetAction -= OnDamageTarget;
                _attacker.PlayAttackSoundAction -= OnPlayAttackSound;
            }

            if (_health != null)
            {
                _health.OnDead -= OnDead;
            }
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
            _attackAudioEvent.Play(_audioSource);
        }
    }
}