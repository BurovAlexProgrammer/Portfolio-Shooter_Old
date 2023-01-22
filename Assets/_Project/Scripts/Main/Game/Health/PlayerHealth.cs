using _Project.Scripts.Extension.Attributes;
using UnityEngine;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game.Health
{
    public class PlayerHealth : HealthBase
    {
        [SerializeField, ReadOnlyField] private AudioSource _audioSource;
        [SerializeField] private AudioEvent _gameOverAudioEvent;
        [SerializeField] private AudioEvent _takeDamageAudioEvent;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (CurrentValue == 0f)
            {
                SetValue(MaxValue);
            }

            Changed += OnChangedHealth;
            Dead += OnLifeEnd;
        }

        private void OnDisable()
        {
            Changed -= OnChangedHealth;
            Dead -= OnLifeEnd;
        }
        
        private void OnLifeEnd()
        {
            _gameOverAudioEvent.Play(_audioSource);
            GameManagerService.GameOver();
        }
        
        private void OnChangedHealth(HealthBase health)
        {
            _takeDamageAudioEvent.Play(_audioSource);
        }
    }
}