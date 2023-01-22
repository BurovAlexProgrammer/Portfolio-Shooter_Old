using Cysharp.Threading.Tasks;
using UnityEngine;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game.Weapon
{
    public abstract class GunBase: MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ShellBase _shellPrefab;

        private float _shootTimer;

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
            var shell = PoolService.Get(_shellPrefab).GetComponent<ShellBase>();
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