﻿using System;
using _Project.Scripts.Main.Wrappers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Main.Game.Weapon
{
    public abstract class GunBase: MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [FormerlySerializedAs("_shellPrefab")] [SerializeField] private ShellBase _prefab;
        private MonoPool<ShellBase> _shellPool;
        
        private float _shootTimer;

        private void Start()
        {
            _shellPool = new MonoPool<ShellBase>(_prefab, null, 10, 12);
        }

        public virtual void TryShoot()
        {
            if (_shootTimer <= 0f)
            {
                Shoot();
                _shootTimer = _weaponConfig.FireRateDelay;
                _ = RunTimer();
                return;
            }
        }

        protected virtual void Shoot()
        {
            var shell = _shellPool.Get().GetComponent<ShellBase>();
            shell.Shoot(transform);
            shell.DestroyOnLifetimeEnd();
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