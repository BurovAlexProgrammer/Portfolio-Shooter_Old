using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class StatisticService : BaseService
    {
        private Dictionary<StatisticData.RecordName, StatisticData.RecordItem> _statisticRecords;
        private string[] _fieldNames;
        private string _storedFolder;
        private string _storedFolderPath;

        public void Init()
        {
            _storedFolder ??= Application.dataPath + "/StoredData/";
            _storedFolderPath = _storedFolder + "Statistic.data";
            _fieldNames = Enum.GetNames(typeof(StatisticData.RecordName));
            // _statisticRecords = Enum.GetValues(typeof(StatisticData.RecordName))
            //     .Cast<StatisticData.RecordName>()
            //     .ToDictionary(key => key, value => new StatisticData.RecordItem(value.ToString(), value.));
            LoadFromFile();
        }

        public void LoadFromFile()
        {
            Directory.CreateDirectory(_storedFolder);
            
            try
            {
                if (!File.Exists(_storedFolderPath))
                {
                    Debug.LogWarning($"Stored file '{_storedFolderPath}' not found. Created empty statistic file.");
                    var emptyRecords = JsonConvert.SerializeObject(StatisticData.Records);
                    File.WriteAllText(_storedFolderPath, emptyRecords);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
            var json = File.ReadAllText(_storedFolderPath);
            var storedData = JsonConvert.DeserializeObject<Dictionary<StatisticData.RecordName,StatisticData.RecordItem>>(json);
            // else
            {
                
                // 
                // 
                // if (storedData == null)
                // {
                //     Debug.LogWarning($"Stored file '{_storedFilePath}' is corrupted. Default settings using instead.");
                // }
                // _saved.CopyDataFrom(storedData ?? _default);
            }

            // _current ??= ScriptableObject.CreateInstance<T>();
            // _current.CopyDataFrom(_saved);
        }

        // public void SaveToFile()
        // {
        //     var data = Serializer.ToJson(_current);
        //     File.WriteAllText(_storedFilePath, data);
        // }
    }
}