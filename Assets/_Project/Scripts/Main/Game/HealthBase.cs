using System;
using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    public abstract class HealthBase : MonoBeh
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _currentValue;
        
        public Action LifeEnd;
        public Action GotDamage;

        public float MaxValue => _maxValue;
        public float CurrentValue => _currentValue;

        public void Init(float currentHealth, float maxHealth)
        {
            _currentValue = currentHealth;
            _maxValue = maxHealth;
        }
        
        public void SetValue(float value)
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
                LifeEnd?.Invoke();
            }
            else
            {
                GotDamage?.Invoke();
            }
        }
    }
}