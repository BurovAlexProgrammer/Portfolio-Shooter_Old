using System.Collections.Generic;
using Main.Settings;
using Main.Contexts;
using Main.Services;
using UnityEngine;
using AudioSettings = Main.Settings.AudioSettings;

namespace Main.Services
{
    public class SettingsService : MonoBehaviour, IService, IConstructInstaller
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

        public void Construct(IServiceInstaller installer)
        {
            var serviceInstaller = installer as SettingsServiceInstaller;
            _videoSettings = serviceInstaller.VideoSettings;
                _audioSettings = serviceInstaller.AudioSettings;
            _gameSettings = serviceInstaller.GameSettings;
            _audioService = Context.GetService<AudioService>();
            _screenService = Context.GetService<ScreenService>();
            _fileService = Context.GetService<FileService>();
            Init();
        }
    }
}