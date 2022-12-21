using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Main.Localizations
{
    public class Localization
    {
        private Locales _locale;
        private LocalizationInfo _info;
        private string _hint;
        private Dictionary<string, LocalizedItem> _localizedItems;
        private string _filePathInEditor;

        public Locales Locale => _locale;
        public LocalizationInfo Info => _info;
        public Dictionary<string, LocalizedItem> LocalizedItems => _localizedItems;
        public string FilePathInEditor => _filePathInEditor;
        public string Hint => _hint;

        public Localization() {}

        public Localization(Localization other)
        {
            _locale = other._locale;
            _localizedItems = other._localizedItems;
            _filePathInEditor = other._filePathInEditor;
            _hint = other._hint;
            _info = new LocalizationInfo(other.Info);
        }
        
        public Localization(string locale, string hint, string infoJson, string[] lines, string filePathInEditor)
        {
            _locale = ParseLocale(locale);
            _info = JsonConvert.DeserializeObject<LocalizationInfo>(infoJson);
            _localizedItems = new Dictionary<string, LocalizedItem>();
            _filePathInEditor = filePathInEditor;
            
            foreach (var line in lines)
            {
                var localizedItem = ParseLine(line);
                if (localizedItem == null) continue;
                if (_localizedItems.ContainsKey(localizedItem.Key))
                {
                    Debug.Log($"Key {localizedItem.Key} is exist in a dictionary already.");
                    continue;
                }
                _localizedItems.Add(localizedItem.Key, localizedItem);
            }
        }

        private LocalizedItem ParseLine(string line)
        {
            var localizedItem = new LocalizedItem();
            var items = line.Split(";");
            if (items.Length < 4)
            {
                return null;
            }
            localizedItem.Key = items[0];
            localizedItem.Description = items[1];
            localizedItem.Original = items[2];
            localizedItem.Text = items[3];
            return localizedItem;
        }

        private Locales ParseLocale(string localeLine)
        {
            return Enum.Parse<Locales>(localeLine, true);
        }
    }

    public enum Locales
    {
        en_EN,
        ru_RU,
    }
}
