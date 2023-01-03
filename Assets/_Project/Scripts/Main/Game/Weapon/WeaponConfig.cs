using _Project.Scripts.Main.Audio;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    [CreateAssetMenu(menuName = "Custom/Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private float _fireRateDelay;
        [SerializeField] private SimpleAudioEvent _shootAudioEvent;

        public float FireRateDelay => _fireRateDelay;
        public SimpleAudioEvent ShootAudioEvent => _shootAudioEvent;
    }
}