using System;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public abstract class HealthBase : MonoBeh
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _currentValue;

        public Action Dead;
        public Action<HealthBase> Changed;

        public float MaxValue => _maxValue;
        public float CurrentValue => _currentValue;

        public void Init(float currentHealth, float maxHealth)
        {
            _currentValue = currentHealth;
            _maxValue = maxHealth;
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
                Dead?.Invoke();
            }
            else
            {
                Changed?.Invoke(this);
            }
        }
    }
}