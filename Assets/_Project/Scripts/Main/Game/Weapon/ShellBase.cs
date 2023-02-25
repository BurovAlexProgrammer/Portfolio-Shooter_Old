using System.Threading;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.AppServices.PoolService;
using _Project.Scripts.Main.AppServices.SceneServices.PoolService;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.Installers;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ShellBase : MonoPoolItemBase
    {
        [SerializeField] private Destruction _destructionPrefab;
        [SerializeField] private ShellConfig _shellConfig;
        [SerializeField] private float _lifeTime = 5f;

        private CancellationToken _cancellationToken;
        private IPoolService _poolService;
        private GameObject _gameObject;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private bool _collided;

        private void Awake()
        {
            _gameObject = gameObject;
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _poolService = Contexts.GamePlayContext.PoolService;
            _cancellationToken = _gameObject.GetCancellationTokenOnDestroy();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_collided) return;

            _collided = true;
            var targetHealthRef = collision.gameObject.GetComponent<HealthRef>();

            if (targetHealthRef != null)
            {
                TakeDamage(targetHealthRef.Health);
                Destruct();
                return;
            }
            
            var targetHealth = collision.gameObject.GetComponent<HealthBase>();
            
            if (targetHealth != null)
            {
                TakeDamage(targetHealth);
            }

            Destruct();
        }

        public void Shoot(Transform startPoint)
        {
            _collided = false;
            _gameObject.SetActive(true);
            _transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
           _rigidbody.velocity = transform.forward * _shellConfig.InitSpeed;
        }

        public void DestroyOnLifetimeEnd()
        {
            _ = DestroyOnLifetimeEndTask();
        }

        private async UniTask DestroyOnLifetimeEndTask()
        {
            await _lifeTime.WaitInSeconds(PlayerLoopTiming.Update, _cancellationToken);
            
            if (!_gameObject.IsDestroyed()) return;
            
            Destruct();
        }

        private void Destruct()
        {
            var destruction = _poolService.Get(_destructionPrefab);
            var rigidbodies = destruction.GetComponentsInChildren<Rigidbody>();
            destruction.transform.position = _transform.position;
            destruction.transform.rotation = _transform.rotation;
            destruction.gameObject.SetActive(true);

            for (var i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].velocity = _rigidbody.velocity / 5f;
            }

            ReturnToPool();
        }

        private void TakeDamage(HealthBase target)
        {
            target.TakeDamage(_shellConfig.Damage);
        }
    }
}