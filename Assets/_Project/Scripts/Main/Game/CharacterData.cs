using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    [CreateAssetMenu(menuName = "Custom/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private int _score = 1;
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _acceleration = 8f;
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private float _meleeRange = 2.5f;
        [SerializeField] private float _meleeDamage = 5f;

        public float Speed => _speed;
        public float MeleeRange => _meleeRange;
        public float MeleeDamage => _meleeDamage;
        public float Acceleration => _acceleration;
        public float Health => _health;
        public float AttackDelay => _attackDelay;
        public int Score => _score;
    }
}