using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoServiceBase
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioMixerGroup _soundEffectMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        [SerializeField] private AudioClip[] _battlePlaylist;
        [SerializeField] private AudioClip[] _menuPlaylist;

        private ScreenService _screenService;

        private MusicPlayerState _currentState;

        [Inject]
        public void Construct(ScreenService screenService)
        {
            _screenService = screenService;
            _audioListener = _screenService.MainCamera.GetComponent<AudioListener>();
            _musicAudioSource = GetComponent<AudioSource>();
        }

        public void Setup(SettingsService settingsService)
        {
            _musicAudioSource.enabled = settingsService.Audio.MusicEnabled;
            SwitchSoundEffects(settingsService.Audio.SoundEnabled);
        }

        public async UniTaskVoid PlayMusic(MusicPlayerState playerState)
        {
            if (_currentState == playerState) return;
            
            var lastState = playerState;
            _currentState = playerState;
            PlayRandomTrack();

            while (_currentState == lastState)
            {
                await UniTask.WaitForFixedUpdate();
                if (_musicAudioSource != null && _musicAudioSource.isPlaying == false)
                {
                    PlayRandomTrack();
                } 
            }
        }

        private async void SwitchSoundEffects(bool newState)
        {
            await UniTask.NextFrame();
            _soundEffectMixerGroup.audioMixer.SetFloat(_soundEffectMixerGroup.name, newState ? 0f : -80f);
        }

        private void PlayRandomTrack()
        {
            if (_musicAudioSource.isActiveAndEnabled == false) return;
            
            switch (_currentState)
            {
                case MusicPlayerState.None:
                    StopMusic();
                    break;
                case MusicPlayerState.MainMenu:
                    if (_menuPlaylist.Length == 0)
                    {
                        Debug.LogWarning("No audio clips on menuPlayList", this);
                    }
                    else
                    {
                        _musicAudioSource.clip = _menuPlaylist.GetRandomItem();
                        _musicAudioSource.Play();
                    }
                    break;
                case MusicPlayerState.Battle:
                    if (_battlePlaylist.Length == 0)
                    {
                        Debug.LogWarning("No audio clips on battlePlayList", this);
                    }
                    else
                    {
                        _musicAudioSource.clip = _battlePlaylist.GetRandomItem();
                        _musicAudioSource.Play();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void StopMusic()
        {
            _currentState = MusicPlayerState.None;
        }

        public enum MusicPlayerState {None, MainMenu, Battle}
    }
}