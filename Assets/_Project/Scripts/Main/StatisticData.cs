using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static Main.StatisticData.RecordName;

namespace Main
{
    [Serializable]
    public class StatisticData
    {
        public Dictionary<RecordName, string> CommonRecords;
        
        [NonSerialized]
        public Dictionary<RecordName, string> SessionRecords;
        
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
        
        public void ResetSessionData()
        {
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
            LongestGameSessionDuration,
            AverageGameSessionDuration,
            FireCount,
            Movement,
            KillMonsterCount,
            LastGameSessionDuration,
            Scores,
            MaxScores,
        }

        public enum DataType
        {
            Bool,
            Integer,
            Float,
            String,
        }

        public enum FormatType
        {
            Common, Session
        }
        
        private static readonly Dictionary<RecordName, DataType> RecordTypes = new ()
        {
            {GameSessionCount, DataType.Integer},
            {LongestGameSessionDuration, DataType.Float},
            {AverageGameSessionDuration, DataType.Float},
            {FireCount, DataType.Integer},
            {Movement, DataType.Float},
            {KillMonsterCount, DataType.Integer},
            {LastGameSessionDuration, DataType.Float},
            {Scores, DataType.Integer},
            {MaxScores, DataType.Integer},
        };
    }
}