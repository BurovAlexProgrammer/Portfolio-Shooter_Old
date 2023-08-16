using Main.Game.Weapon;
using Main.Wrappers;
using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main.Game.Health
{
    [DisallowMultipleComponent]
    public class SimpleHealth : HealthBase
    {
        [SerializeField] private Destruction _destructionPrefab;
        
        private IPoolService _poolService;
        
        private BasePoolItem _poolItem;

        private void Awake()
        {
            _poolService = Context.GetService<PoolService>();
            _poolItem = GetComponent<BasePoolItem>();
        }

        private void OnEnable()
        {
            if (CurrentValue == 0f)
            {
                SetValue(MaxValue);
            }

            OnDead += Destruct;
        }

        private void OnDisable()
        {
            OnDead -= Destruct;
        }

        private void Destruct()
        {
            if (_destructionPrefab != null)
            {
                var enemyParts = _poolService.GetAndActivate(_destructionPrefab);
                enemyParts.GameObject.transform.position = transform.position;
                enemyParts.GameObject.transform.rotation = transform.rotation;
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