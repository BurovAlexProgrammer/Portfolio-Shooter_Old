using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    public abstract class BaseGun: MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private ObjectPool _objectPool;
        
        private float _shootTimer;

        public virtual void TryShoot()
        {
            Debug.Log("Gun tryShoot");
            if (_shootTimer <= 0f)
            {
                Shoot();
                _shootTimer = _weaponConfig.FireRateDelay;
                return;
            }

            _shootTimer -= Time.deltaTime;
        }

        protected virtual void Shoot()
        {
            var shell = _objectPool.Get().GetComponent<BaseShell>();
            shell.Shoot(transform);
        }
    }
}