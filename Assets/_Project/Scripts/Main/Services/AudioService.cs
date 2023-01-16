using System;
using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : BaseService
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioClip[] _battlePlaylist;
        [FormerlySerializedAs("_backgroundPlaylist")] [SerializeField] private AudioClip[] _menuPlaylist;

        [Inject] private ScreenService _screenService;
        [Inject] private SettingsService _settingsService;

        private MusicPlayerState _currentState;
        
        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _audioListener = _screenService.MainCamera.GetComponent<AudioListener>();
            _musicAudioSource = GetComponent<AudioSource>();
        }

        public void Setup()
        {
            _musicAudioSource.enabled = _settingsService.Audio.MusicEnabled;
        }

        public async UniTask PlayMusic(MusicPlayerState playerState)
        {
            if (_currentState == playerState) return;
            
            var lastState = playerState;
            PlayRandomTrack();

            while (_currentState == lastState)
            {
                UniTask.WaitForFixedUpdate();
                if (_musicAudioSource.isPlaying == false)
                {
                    PlayRandomTrack();
                } 
            }
        }

        private void PlayRandomTrack()
        {
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
        
        private async void TurnDownMusic()
        {
            
        }

        public enum MusicPlayerState {None, MainMenu, Battle}
    }
}