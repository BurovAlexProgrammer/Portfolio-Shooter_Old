using UnityEngine;

namespace Main.Game.Weapon
{
    [CreateAssetMenu(menuName = "Custom/Shell")]
    public class ShellConfig : ScriptableObject
    {
        [SerializeField] private float _initSpeed;
        [SerializeField] private float _damage;

        public float InitSpeed => _initSpeed;
        public float Damage => _damage;
    }
}