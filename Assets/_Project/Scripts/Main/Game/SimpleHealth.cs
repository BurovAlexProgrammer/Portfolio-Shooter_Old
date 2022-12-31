using _Project.Scripts.Main.Game.Weapon;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public class SimpleHealth : HealthBase
    {
        [SerializeField] private Destruction _destructionPrefab;
        
        private void Start()
        {
            if (CurrentValue == 0f)
            {
                SetValue(MaxValue);
            }

            LifeEnd += Destruct;
        }

        private void Destruct()
        {
            if (_destructionPrefab == null) return;

            Instantiate(_destructionPrefab, transform.parent);
            Destroy(gameObject);
        }
    }
}