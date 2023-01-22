using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static _Project.Scripts.Main.StatisticData.RecordName;

namespace _Project.Scripts.Main
{
    [Serializable]
    public class StatisticData
    {
        public Dictionary<RecordName, string> CommonRecords;
        public Dictionary<RecordName, string> SessionRecords;
        public string str = "";
        
        private static string DefaultRecordValue(RecordName recordName) =>
            RecordTypes[recordName] switch
            {
                DataType.Bool => "false",
                DataType.Float => "0",
                DataType.Integer => "0",
                DataType.String => "",
                _ => throw new ArgumentOutOfRangeException()
            };

        public StatisticData()
        {
            SetDefaultRecords(out CommonRecords);
            SetDefaultRecords(out SessionRecords);
        }

        private void SetDefaultRecords(out Dictionary<RecordName, string> dictionary)
        {
            dictionary =               
                Enum.GetValues(typeof(RecordName))
                .Cast<RecordName>()
                .ToDictionary(key => key, DefaultRecordValue);
        }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RecordName
        {
            GameSessionCount,
            LongestGameSession,
            AverageLongGameSession,
            FireCount,
            Movement,
            KillMonsterCount,
        }

        public enum DataType
        {
            Bool,
            Integer,
            Float,
            String,
        }
        
        private static readonly Dictionary<RecordName, DataType> RecordTypes = new ()
        {
            {GameSessionCount, DataType.Integer},
            {LongestGameSession, DataType.Float},
            {AverageLongGameSession, DataType.Float},
            {FireCount, DataType.Integer},
            {Movement, DataType.Float},
            {KillMonsterCount, DataType.Integer},
        };
    }
}