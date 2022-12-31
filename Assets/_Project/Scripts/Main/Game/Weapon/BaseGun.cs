using System;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    public abstract class BaseGun: MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private BaseShell _shellPrefab;
        private MonoPool<BaseShell> _shellPool;
        
        private float _shootTimer;

        private void Start()
        {
            _shellPool = new MonoPool<BaseShell>(_shellPrefab, null, 10, 12);
        }

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
            var shell = _shellPool.Get().GetComponent<BaseShell>();
            shell.Shoot(transform);
            shell.DestroyOnLifetimeEnd();
        }
    }
}