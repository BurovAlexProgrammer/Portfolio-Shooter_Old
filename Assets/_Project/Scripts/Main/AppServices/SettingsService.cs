using System.Collections.Generic;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Settings;
using UnityEngine;
using Zenject;
using AudioSettings = _Project.Scripts.Main.Settings.AudioSettings;

namespace _Project.Scripts.Main.AppServices
{
    public class SettingsService : MonoServiceBase
    {
        [SerializeField] private SettingGroup<VideoSettings> _videoSettings;
        [SerializeField] private SettingGroup<AudioSettings> _audioSettings;
        [SerializeField] private SettingGroup<GameSettings> _gameSettings;

        private AudioService _audioService;
        private ScreenService _screenService;
        private IFileService _fileService;
        private List<ISettingGroup> _settingList;

        public VideoSettings Video => _videoSettings.CurrentSettings;
        public AudioSettings Audio => _audioSettings.CurrentSettings;
        public GameSettings GameSettings => _gameSettings.CurrentSettings;
        public AudioService AudioService => _audioService;
        public ScreenService ScreenService => _screenService;
        public IFileService FileService => _fileService;

        [Inject]
        protected void Construct(AudioService audioService, ScreenService screenService, IFileService fileService)
        {
            _audioService = audioService;
            _screenService = screenService;
            _fileService = fileService;
            Init();
        }
        
        private void Init()
        {
            _settingList = new List<ISettingGroup>
            {
                _audioSettings,
                _videoSettings,
                _gameSettings,
            };

            foreach (var settingGroup in _settingList)
            {
                settingGroup.Init(this);
            }
        }

        public void Load()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.LoadFromFile();
            }
        }

        public void Save()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.SaveToFile();
                settingGroup.Init(this);
                settingGroup.LoadFromFile();
            }
        }

        public void Cancel()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Cancel();
            }
        }

        public void Restore()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.Restore();
            }
        }

        public void Apply()
        {
            foreach (var settingGroup in _settingList)
            {
                settingGroup.ApplySettings(this);
            }
        }
    }
}