using _Project.Scripts.Main.Game.Health;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public class HealthRef : MonoBehaviour
    {
        [SerializeField] private HealthBase _health;

        public HealthBase Health => _health;
    }
}