using System;
using Main.Extension;
using Main.Services;
using Cysharp.Threading.Tasks;
using sm_application.Scripts.Main.Wrappers;
using smApplication.Scripts.Extension;
using UnityEngine;
using UnityEngine.Audio;

namespace Main.Services
{
    public class AudioService : IService, IConstructInstaller
    {
        private AudioServiceInstaller _serviceInstaller;
        private AudioListener _audioListener;
        private AudioSource _musicAudioSource;
        private AudioMixerGroup _soundEffectMixerGroup;
        private AudioMixerGroup _musicMixerGroup;
        private AudioClip[] _battlePlaylist;
        private AudioClip[] _menuPlaylist;

        private MusicPlayerState _currentState;
        
        public enum MusicPlayerState {None, MainMenu, Battle}

        public AudioListener AudioListener => _audioListener;
        
        public void Construct(IServiceInstaller installer)
        {
            _serviceInstaller = installer.Install() as AudioServiceInstaller;
            _audioListener = _serviceInstaller.AudioListener;
            _musicAudioSource = _serviceInstaller.MusicAudioSource;
            _soundEffectMixerGroup = _serviceInstaller.SoundEffectMixerGroup;
            _musicMixerGroup = _serviceInstaller.MusicMixerGroup;
            _battlePlaylist = _serviceInstaller.BattlePlaylist;
            _menuPlaylist = _serviceInstaller.MenuPlaylist;
        }

        public void Setup(SettingsService settingsService)
        {
            _musicAudioSource.enabled = settingsService.Audio.MusicEnabled;
            SwitchSoundEffects(settingsService.Audio.SoundEnabled);
        }

        public void PlayMusic(MusicPlayerState musicPlayerState)
        {
            if (_currentState == musicPlayerState) return;

            PlayMusicAsync(musicPlayerState).Forget();
        }

        private async UniTask PlayMusicAsync(MusicPlayerState musicPlayerState)
        {
            var lastState = musicPlayerState;
            _currentState = musicPlayerState;
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
                        Log.Warn("No audio clips on menuPlayList", _serviceInstaller);
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
                        Log.Warn("No audio clips on battlePlayList", _serviceInstaller);
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
    }
}