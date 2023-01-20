using _Project.Scripts.Extension.Attributes;
using UnityEngine;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game
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

            GotDamageAction += OnGotDamage;
            LifeEndAction += OnLifeEnd;
        }

        private void OnDisable()
        {
            GotDamageAction -= OnGotDamage;
            LifeEndAction -= OnLifeEnd;
        }
        
        private void OnLifeEnd()
        {
            _gameOverAudioEvent.Play(_audioSource);
            GameManagerService.GameOver();
        }
        
        private void OnGotDamage()
        {
            _takeDamageAudioEvent.Play(_audioSource);
        }
    }
}