using Main.Game.Health;
using UnityEngine;

namespace Main.Game
{
    public class HealthRef : MonoBehaviour
    {
        [SerializeField] private HealthBase _health;

        public HealthBase Health => _health;
    }
}