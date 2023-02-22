using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game.Weapon;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Game.Health
{
    public class SimpleHealth : HealthBase
    {
        [SerializeField] private Destruction _destructionPrefab;
        
        [Inject] private PoolService _poolService;
        
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

            Dead += Destruct;
        }

        private void OnDisable()
        {
            Dead -= Destruct;
        }

        private void Destruct()
        {
            if (_destructionPrefab != null)
            {
                var enemyParts = _poolService.GetAndActivate(_destructionPrefab);
                enemyParts.Transform.position = Transform.position;
                enemyParts.Transform.rotation = Transform.rotation;
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