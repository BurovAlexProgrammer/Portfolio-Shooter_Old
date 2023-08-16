using System;
using UnityEngine;

namespace Main.Game.Health
{
    public abstract class HealthBase : MonoBehaviour
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _currentValue;

        public event Action OnDead;
        public event Action<HealthBase> OnChanged;

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
            if (value < 0f) throw new Exception(Messages.TakeDamageCannotBeNegative);
            
            if (_currentValue <= 0f || value == 0f) return;

            _currentValue -= value;

            if (_currentValue <= 0f)
            {
                _currentValue = 0f;
                OnChanged?.Invoke(this);
                OnDead?.Invoke();
                return;
            } 
            
            OnChanged?.Invoke(this);
        }
    }
}