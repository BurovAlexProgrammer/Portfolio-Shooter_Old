using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Localizations;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    [UsedImplicitly]
    public class LocalizationService : IService
    {
        private Locales _currentLocale;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _currentLocalization;
        private bool _isLoaded;

        public Dictionary<Locales, Localization> Localizations => _localizations;
        public bool IsLoaded => _isLoaded;

        
        private SettingsService _settingsService;

        [Inject]
        public void Construct(SettingsService settingsService)
        {
            _settingsService = settingsService;
            _currentLocale = _settingsService.GameSettings.CurrentLocale;
            _localizations = new Dictionary<Locales, Localization>();
            this.RegisterService();
            Init();
        }

        public async UniTask<Dictionary<Locales, Localization>> GetLocalizationsAsync()
        {
            while (!_isLoaded)
            {
                await UniTask.NextFrame();
            }

            return _localizations;
        }

        private async void Init()
        {
            var resources = await Addressables.LoadResourceLocationsAsync("locale").Task;
            var localeAssets = GetLocaleAssets(resources);
            var tasks = LoadAssets(localeAssets);
            var textAssets = await Task.WhenAll(tasks);

            for (var i = 0; i < textAssets.Length; i++)
            {
                var filePath = resources[i].ToString();
                var localization = LoadLocaleFile(textAssets[i], filePath);
                _localizations.Add(localization.Locale, localization);
            }

            if (!_localizations.ContainsKey(_currentLocale))
                throw new Exception("Current localization not found.");

            _currentLocalization = _localizations[_currentLocale];
            _isLoaded = true;
            return;

            List<IResourceLocation> GetLocaleAssets(IList<IResourceLocation> resourceLocations) =>
                resourceLocations.Where(x => x.ToString().Contains(".csv")).ToList();

            List<Task<TextAsset>> LoadAssets(List<IResourceLocation> list) => 
                list.Select(x => Addressables.LoadAssetAsync<TextAsset>(x).Task).ToList();
        }

        private Localization LoadLocaleFile(TextAsset textAsset, string filePath)
            {
                var lines = textAsset.text.SplitLines();
                var locale = lines[0];
                var formatInfoMaybeJson = lines[1];
                var hint = lines[2];

                var itemList = new List<string>();
                for (var i = 3; i < lines.Length; i++)
                {
                    itemList.Add(lines[i]);
                }

                return new Localization(locale, hint, formatInfoMaybeJson, itemList.ToArray(), filePath);
            }

            public string GetLocalizedText(string key)
            {
                if (!_currentLocalization.LocalizedItems.ContainsKey(key))
                {
                    AddNewKeyToDictionary(key);
                }

                return _currentLocalization.LocalizedItems[key].Text;
            }

            private void AddNewKeyToDictionary(string newKey)
            {
                if (Application.isEditor)
                {
                    Debug.LogError($"Key '{newKey}' not in current locale '{_currentLocale.ToString()}'.");
                    var localizations = _localizations.Select((x) => x.Value);
                    foreach (var localization in localizations)
                    {
                        if (localization.LocalizedItems.ContainsKey(newKey) == false)
                        {
                            Debug.LogWarning(
                                $"Key '{newKey}' is not in locale '{localization.Locale.ToString()}'. Adding new key..");
                            var fullPath = Path.Combine(Application.dataPath, @"..\") +
                                           localization.FilePathInEditor;
                            using var streamWriter = File.AppendText(fullPath);
                            streamWriter.WriteLine($"{newKey};;;key.{newKey};");
                            var newLocalizedItem = new LocalizedItem { Key = newKey, Text = $"key^{newKey}" };
                            localization.LocalizedItems.Add(newKey, newLocalizedItem);
                        }
                    }
                }
                else
                {
                    Debug.LogError($"Key '{newKey}' not in current locale '{_currentLocale.ToString()}'");
                }
            }
        }
    }