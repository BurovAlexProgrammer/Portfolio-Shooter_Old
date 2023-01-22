using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class StatisticService : BaseService
    {
        private StatisticData _statisticData;
        private string[] _fieldNames;
        private string _storedFolder;
        private string _storedFolderPath;

        public void Init()
        {
            _statisticData = new StatisticData();
            _storedFolder ??= Application.dataPath + "/StoredData/";
            _storedFolderPath = _storedFolder + "Statistic.data";
            _fieldNames = Enum.GetNames(typeof(StatisticData.RecordName));
            LoadFromFile();
        }

        public void LoadFromFile()
        {
            Directory.CreateDirectory(_storedFolder);

            if (!File.Exists(_storedFolderPath))
            {
                Debug.LogWarning($"Stored file '{_storedFolderPath}' not found. Created empty statistic file.");
                var emptyRecords = JsonConvert.SerializeObject(_statisticData);
                File.WriteAllText(_storedFolderPath, emptyRecords);
            }

            var json = File.ReadAllText(_storedFolderPath);
            _statisticData = JsonConvert.DeserializeObject<StatisticData>(json);
        }

        public void SaveToFile()
        {
            var data = JsonConvert.SerializeObject(_statisticData);
            File.WriteAllText(_storedFolderPath, data);
        }
    }
}