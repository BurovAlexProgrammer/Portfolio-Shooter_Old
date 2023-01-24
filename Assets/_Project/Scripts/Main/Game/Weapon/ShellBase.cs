using _Project.Scripts.Extension;
using _Project.Scripts.Main.Game.Health;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ShellBase : MonoPoolItemBase
    {
        [SerializeField] private ShellConfig _shellConfig;
        [SerializeField] private Destruction _destructionPrefab;
        [SerializeField] private float _lifeTime = 5f;

        private Rigidbody _rigidbody;
        private bool _collided;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
            gameObject.SetActive(true);
           _transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
           _rigidbody.velocity = _transform.forward * _shellConfig.InitSpeed;
        }

        public void DestroyOnLifetimeEnd()
        {
            _ = DestroyOnLifetimeEndTask();
        }

        private async UniTask DestroyOnLifetimeEndTask()
        {
            await _lifeTime.WaitInSeconds();
            
            if (!_gameObject.activeSelf) return;
            
            Destruct();
        }

        private void Destruct()
        {
            var destruction = Services.Services.PoolService.Get(_destructionPrefab);
            var rigidbodies = destruction.GetComponentsInChildren<Rigidbody>();
            destruction._transform.position = _transform.position;
            destruction._transform.rotation = _transform.rotation;
            destruction._gameObject.SetActive(true);
            
            foreach (var rb in rigidbodies)
            {
                rb.velocity = _rigidbody.velocity / 5f;
            }

            ReturnToPool();
        }

        private void TakeDamage(HealthBase target)
        {
            target.TakeDamage(_shellConfig.Damage);
        }
    }
}