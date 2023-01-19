using System;
using _Project.Scripts.Main.Game.Weapon;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game
{
    public class SimpleHealth : HealthBase
    {
        [SerializeField] private Destruction _destructionPrefab;

        private MonoPoolItemBase _poolItem;

        private void Awake()
        {
            _poolItem = GetComponent<MonoPoolItemBase>();
        }

        private void OnEnable()
        {
            if (CurrentValue == 0f)
            {
                SetValue(MaxValue);
            }

            LifeEnd += Destruct;
        }

        private void OnDisable()
        {
            LifeEnd -= Destruct;
        }

        private void Destruct()
        {
            if (_destructionPrefab != null)
            {
                var enemyParts = PoolService.GetAndActivate(_destructionPrefab);
                enemyParts._transform.position = _transform.position;
                enemyParts._transform.rotation = _transform.rotation;
            }

            if (_poolItem != null)
            {
                _poolItem.ReturnToPool();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}