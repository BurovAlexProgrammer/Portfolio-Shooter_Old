using System;
using System.IO;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Wrappers;
using Newtonsoft.Json;
using UnityEngine;
using static _Project.Scripts.Extension.Common;
using Debug = UnityEngine.Debug;

namespace _Project.Scripts.Main.Settings
{
    [Serializable]
    public class SettingGroup<T> : ISettingGroup where T : SettingsSO
    {
        [SerializeField] private T _default;
        [SerializeField] private T _saved;
        [SerializeField] private T _current;

        private IFileService _fileService;
        
        public T CurrentSettings => _current;

        private string _settingsFilePath;

        public void Init(SettingsService settingsService)
        {
            _fileService = settingsService.FileService;
            LoadFromFile();
            ApplySettings(settingsService);
        }

        public void ApplySettings(SettingsService settingsService)
        {
            _saved.ApplySettings(settingsService);
        }

        public void LoadFromFile()
        {
            if (!Directory.Exists(_fileService.StorageFolder))
            {
                Directory.CreateDirectory(_fileService.StorageFolder);
            }
            
            _settingsFilePath = Path.Combine(_fileService.StorageFolder,  $"Stored_{typeof(T).Name}.data");
            _saved = ScriptableObject.CreateInstance<T>();

            if (!File.Exists(_settingsFilePath))
            {
                CreateDefaultSettingFile();
                Debug.LogWarning($"Stored file '{_settingsFilePath}' not found. Default settings using instead.");
                _saved = _default;
            }
            else
            {
                var json = File.ReadAllText(_settingsFilePath);
                var storedData = JsonConvert.DeserializeObject<T>(json, new ScriptableObjectConverter<T>());
                if (storedData == null)
                {
                    Debug.LogWarning($"Stored file '{_settingsFilePath}' is corrupted. Default settings using instead.");
                }
                _saved = storedData ?? _default;
            }

            _current ??= ScriptableObject.CreateInstance<T>();
            _current = _saved;
        }

        public void SaveToFile()
        {
            var data = Serializer.ToJson(_current);
            File.WriteAllText(_settingsFilePath, data);
        }

        public void Cancel()
        {
            _current = _saved;
        }

        public void Restore()
        {
            _current = _default;
            _saved = _default;
        }

        private void CreateDefaultSettingFile()
        {
            var data = Serializer.ToJson(_default);
            File.WriteAllText(_settingsFilePath, data);
        }
    }
}