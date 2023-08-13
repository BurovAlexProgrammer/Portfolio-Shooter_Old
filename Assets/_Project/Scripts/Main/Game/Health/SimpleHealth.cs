using _Project.Scripts.Main.AppServices.SceneServices.PoolService;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Game.Weapon;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Health
{
    [DisallowMultipleComponent]
    public class SimpleHealth : HealthBase
    {
        [SerializeField] private Destruction _destructionPrefab;
        
        private IPoolService _poolService;
        
        private BasePoolItem _poolItem;

        private void Awake()
        {
            _poolService = GamePlayContext.PoolService;
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
                enemyParts.transform.position = transform.position;
                enemyParts.transform.rotation = transform.rotation;
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