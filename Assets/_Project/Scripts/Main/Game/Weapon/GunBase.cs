using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    public abstract class GunBase: MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ShellBase _shellPrefab;
        private MonoPool<ShellBase> _shellPool;

        private float _shootTimer;

        private void Start()
        {
            _shellPool = new MonoPool<ShellBase>(_shellPrefab, null, 10, 12);
        }

        public virtual void TryShoot()
        {
            if (_shootTimer <= 0f)
            {
                _shootTimer = _weaponConfig.FireRateDelay;
                _ = RunTimer();
                Shoot();
            }
        }

        protected virtual void Shoot()
        {
            var shell = _shellPool.Get().GetComponent<ShellBase>();
            shell.Shoot(transform);
            shell.DestroyOnLifetimeEnd();
            
            if (_audioSource != null)
            {
                _weaponConfig.ShootAudioEvent.Play(_audioSource);
            }
        }

        private async UniTask RunTimer()
        {
            while (_shootTimer > 0f)
            {
                await UniTask.NextFrame();
                _shootTimer -= Time.deltaTime;
            }
        }

        private void OnDestroy()
        {
            Debug.Log("Destroy GUN");
        }
    }
}