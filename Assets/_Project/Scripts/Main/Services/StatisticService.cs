using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using static _Project.Scripts.Main.StatisticData;

namespace _Project.Scripts.Main.Services
{
    public class StatisticService : BaseService
    {
        private StatisticData _statisticData;
        private string _storedFolder;
        private string _storedFolderPath;

        public void Init()
        {
            _statisticData = new StatisticData();
            _storedFolder ??= Application.dataPath + "/StoredData/";
            _storedFolderPath = _storedFolder + "Statistic.data";
            LoadFromFile();
            TimerExecuting();
        }

        public string GetRecord(RecordName recordName)
        {
            return _statisticData.CommonRecords[recordName];
        }

        public void AddValueToRecord(RecordName recordName, int value)
        {
            if (value == 0) return;

            var commonRecordValue = int.Parse(_statisticData.CommonRecords[recordName]);
            _statisticData.CommonRecords[recordName] = (commonRecordValue + value).ToString();
            var sessionRecordValue = int.Parse(_statisticData.SessionRecords[recordName]);
            _statisticData.SessionRecords[recordName] = (sessionRecordValue + value).ToString();
        }

        public void AddValueToRecord(RecordName recordName, float value)
        {
            if (value == 0f) return;

            var commonRecordValue = float.Parse(_statisticData.CommonRecords[recordName]);
            _statisticData.CommonRecords[recordName] = (commonRecordValue + value).ToString();
            var sessionRecordValue = float.Parse(_statisticData.SessionRecords[recordName]);
            _statisticData.SessionRecords[recordName] = (sessionRecordValue + value).ToString();
        }

        public float GetFloatValue(RecordName recordName, FormatType formatType = FormatType.Common)
        {
            return formatType switch
            {
                FormatType.Common => float.Parse(_statisticData.CommonRecords[recordName]),
                FormatType.Session => float.Parse(_statisticData.SessionRecords[recordName]),
                _ => throw new ArgumentOutOfRangeException(nameof(formatType), formatType, null)
            };
        }

        public void ResetSessionRecords()
        {
            _statisticData.ResetSessionData();
        }

        public void SaveToFile()
        {
            var data = JsonConvert.SerializeObject(_statisticData);
            File.WriteAllText(_storedFolderPath, data);
        }
        
        public void CalculateSessionDuration()
        {
            var sessionDuration = GetFloatValue(RecordName.LastGameSessionDuration);
            var longestSession = GetFloatValue(RecordName.LongestGameSessionDuration);
            var averageSession = GetFloatValue(RecordName.AverageGameSessionDuration);
            longestSession = Mathf.Max(longestSession, sessionDuration);
            averageSession = averageSession == 0 ? sessionDuration : (averageSession * 3 + sessionDuration) / 4f;
            SetRecord(RecordName.LongestGameSessionDuration, longestSession.ToString());
            SetRecord(RecordName.AverageGameSessionDuration, averageSession.ToString());
        }
        
        private void SetRecord(RecordName recordName, string value)
        {
            _statisticData.CommonRecords[recordName] = value;
        }

        private async void TimerExecuting()
        {
            var delta = 0f;

            while (this != null)
            {
                await UniTask.NextFrame();
                delta += Time.deltaTime;

                if (delta < 1f) continue;

                if (Services.GameManagerService.CurrentGameState == GameStates.PlayGame ||
                    Services.GameManagerService.CurrentGameState == GameStates.CustomSceneBoot) //TODO Temp remove!! 
                {
                    AddValueToRecord(RecordName.LastGameSessionDuration, delta);
                }

                delta = 0f;
            }
        }

        private void LoadFromFile()
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
    }
}