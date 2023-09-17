using Main.Extension.Attributes;
using Main.Audio;
using Main.Contexts;
using Main.Services;
using UnityEngine;

namespace Main.Game.Health
{
    [DisallowMultipleComponent]
    public class PlayerHealth : HealthBase
    {
        [SerializeField, ReadOnlyField] private AudioSource _audioSource;
        [SerializeField] private AudioEvent _gameOverAudioEvent;
        [SerializeField] private AudioEvent _takeDamageAudioEvent;

        private GameManagerService _gameManager;

        private void Awake()
        {
            _gameManager = Context.Resolve<GameManagerService>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (CurrentValue == 0f)
            {
                SetValue(MaxValue);
            }

            OnChanged += OnChangedHealth;
            OnDead += OnLifeEnd;
        }

        private void OnDisable()
        {
            OnChanged -= OnChangedHealth;
            OnDead -= OnLifeEnd;
        }

        private void OnLifeEnd()
        {
            _gameOverAudioEvent.Play(_audioSource);
            _gameManager.RunGameOver();
        }

        private void OnChangedHealth(HealthBase health)
        {
            _takeDamageAudioEvent.Play(_audioSource);
        }
    }
}