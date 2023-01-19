using System;
using UnityEngine;

namespace _Project.Data.Game
{
    [CreateAssetMenu(menuName = "Custom/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _meleeRange;
        [SerializeField] private float _meleeDamage;

        public float Speed => _speed;
        public float MeleeRange => _meleeRange;

        public float MeleeDamage => _meleeDamage;
    }
}