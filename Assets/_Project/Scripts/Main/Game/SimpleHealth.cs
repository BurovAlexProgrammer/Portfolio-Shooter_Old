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
            if (_destructionPrefab != null)
            {
                var enemyParts = Instantiate(_destructionPrefab, transform.parent);
                enemyParts._transform.position = _transform.position;
                enemyParts._transform.rotation = _transform.rotation;
            }

            Destroy(gameObject);
        }
    }
}