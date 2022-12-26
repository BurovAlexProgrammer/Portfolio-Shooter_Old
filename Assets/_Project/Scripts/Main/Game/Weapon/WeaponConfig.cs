using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    [CreateAssetMenu(menuName = "Custom/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private float _fireRateDelay;

        public float FireRateDelay => _fireRateDelay;
    }
}