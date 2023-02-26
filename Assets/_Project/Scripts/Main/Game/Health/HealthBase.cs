using System;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Health
{
    public abstract class HealthBase : MonoBehaviour
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _currentValue;

        public event Action Dead;
        public event Action<HealthBase> Changed;

        public float MaxValue => _maxValue;
        public float CurrentValue => _currentValue;

        public void Init(float currentHealth, float maxHealth)
        {
            _maxValue = maxHealth;
            _currentValue = Math.Min(currentHealth, maxHealth);
        }

        protected void SetValue(float value)
        {
            _currentValue = value;
        }

        public void TakeDamage(float value)
        {
            if (_currentValue == 0f) return;

            _currentValue -= value;

            if (_currentValue <= 0f)
            {
                _currentValue = 0f;
                Changed?.Invoke(this);
                Dead?.Invoke();
                return;
            } 
            
            Changed?.Invoke(this);
        }
    }
}