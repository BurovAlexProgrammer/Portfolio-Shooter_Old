using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Scripts.Main.Localizations;
using UnityEngine;
using Newtonsoft.Json;

namespace _Project.Scripts.Extension.Editor.LocalizationTools
{
    public class LocalizationTools
    {
        private static LocalizationTools _instance;
        private static LocalizationToolsSettings _settings;
        private Dictionary<Locales, Localization> _localizations;
        private Localization _originalLocalization;
        public static LocalizationTools Instance => _instance ?? new LocalizationTools();
        public Dictionary<Locales, Localization> Localizations => _localizations;
        public Localization OriginalLocalization => _originalLocalization;


        public void AddNewKey(string newKey)
        {
            foreach (var (key, localization) in _localizations)
            {
                if (localization.LocalizedItems.ContainsKey(newKey))
                {
                    Debug.LogWarning($"Localization Key already exist in '{localization.Info.name}'. Skipped to add.");
                    continue;
                }

                using var streamWriter = File.AppendText(localization.FilePathInEditor);
                streamWriter.WriteLine($"{newKey};;;key.{newKey};");
                var newLocalizedItem = new LocalizedItem() { Key = newKey, Text = $"key^{newKey}" };
                localization.LocalizedItems.Add(newKey, newLocalizedItem);
            }
        }

        public void RemoveKey(string removeKey)
        {
            foreach (var (key, localization) in _localizations)
            {
                if (localization.LocalizedItems.ContainsKey(removeKey) == false) continue;

                localization.LocalizedItems.Remove(removeKey);
                
                SaveLocalization(localization);
                Debug.Log($"Key '{removeKey}' removed for localization '{localization.Info.name}'.");
            }
        }
        
        public void SaveLocalization(Localization localizationInstance, bool original = false)
        {
            var fileContentLines = new List<string>
            {
                Enum.GetName(typeof(Locales), localizationInstance.Locale),
                JsonConvert.SerializeObject(localizationInstance.Info, Formatting.None),
                localizationInstance.Hint,
            };
            
            foreach (var (key, localizedItem) in localizationInstance.LocalizedItems)
            {
                localizedItem.Text = original ? localizedItem.Original : localizedItem.Text;
                fileContentLines.Add($"{key};{localizedItem.Description};{localizedItem.Original};{localizedItem.Text};");
            }

            var fileContent = String.Join(Environment.NewLine, fileContentLines.ToArray()) + "\n";
            var filePath = localizationInstance.FilePathInEditor;
            File.WriteAllText(filePath, fileContent);

            if (original)
            {
                ReloadOriginalLocalization();
                UpdateAllOriginals();
            }
        }

        private void UpdateAllOriginals()
        {
            foreach (var (key, localization) in _localizations)
            {
                if (localization.Locale == _originalLocalization.Locale) continue;

                foreach (var (itemKey, localizedItem) in localization.LocalizedItems)
                {
                    if (_originalLocalization.LocalizedItems.ContainsKey(itemKey) == false) continue;
                    localizedItem.Original = _originalLocalization.LocalizedItems[itemKey].Original;
                }
                
                SaveLocalization(localization);
                Debug.Log($"Originals for {localization.Info.name} updated.");
            }
        }
        
        private LocalizationTools()
        {
            _localizations = new Dictionary<Locales, Localization>();
            _settings = Common.LoadSingleAsset<LocalizationToolsSettings>();
            if (_settings == null) throw new Exception("LocalizationToolsSettings asset not found.");
            
            LoadLocalizations();
            _originalLocalization = _localizations.Single(x => x.Key == _settings.OriginalLocale).Value;
        }

        private void ReloadOriginalLocalization()
        {
            var filePath = _originalLocalization.FilePathInEditor;
            var fileLines = File.ReadAllLines(filePath);
            _originalLocalization = Parse(fileLines, filePath); 
        }

        private void LoadLocalizations()
        {
            var filePaths = Directory.GetFiles(_settings.LocalizationStorePath, "*.csv");
            
            foreach (var filePath in filePaths)
            {
                var fileLines = File.ReadAllLines(filePath);
                var newLocalization = Parse(fileLines, filePath); 
                _localizations.Add(newLocalization.Locale, newLocalization);
            }
        }

        private Localization Parse(string[] lines, string filePath)
        {
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
    }
}