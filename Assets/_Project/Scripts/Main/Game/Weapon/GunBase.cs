using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main.Game.Weapon
{
    public abstract class GunBase: MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ShellBase _shellPrefab;

        private float _shootTimer;

        private IPoolService _poolService;

        private void Awake()
        {
            _poolService = Context.Resolve<IPoolService>();
        }

        public virtual bool TryShoot()
        {
            if (_shootTimer <= 0f)
            {
                _shootTimer = _weaponConfig.FireRateDelay;
                _ = RunTimer();
                Shoot();
                return true;
            }

            return false;
        }

        protected virtual void Shoot()
        {
            var shell = _poolService.Get(_shellPrefab).GameObject.GetComponent<ShellBase>();
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
    }
}