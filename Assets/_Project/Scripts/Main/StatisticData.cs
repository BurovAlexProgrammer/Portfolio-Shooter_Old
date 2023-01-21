using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static _Project.Scripts.Main.StatisticData.RecordName;

namespace _Project.Scripts.Main
{
    public static class StatisticData
    {
        public static readonly Dictionary<RecordName, RecordItem> Records;

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

        static StatisticData()
        {
            Records = new Dictionary<RecordName, RecordItem>();
            AddRecord(GameSessionCount, DataType.Integer);
            AddRecord(LongestGameSession, DataType.Float);
            AddRecord(AverageLongGameSession, DataType.Float);
            AddRecord(FireCount, DataType.Integer);
            AddRecord(Movement, DataType.Float);
            AddRecord(KillMonsterCount, DataType.Integer);
        }

        private static void AddRecord(RecordName recordName, DataType dataType)
        {
            Records.Add(recordName, new RecordItem(recordName, dataType));
        }

        public class RecordItem
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public RecordName Name;
            public string Value;
            [JsonConverter(typeof(StringEnumConverter))]
            public DataType DataType;

            private float _floatValue;
            private float _intValue;
            private bool _boolValue;


            public RecordItem(RecordName recordName, DataType dataType)
            {
                DataType = dataType;

                switch (DataType)
                {
                    case DataType.Bool:
                        _boolValue = false;
                        Value = _boolValue.ToString();
                        break;
                    case DataType.Integer:
                        _intValue = 0;
                        Value = _intValue.ToString();
                        break;
                    case DataType.Float:
                        _floatValue = 0f;
                        Value = _floatValue.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
                }
            }

            public RecordItem(RecordName recordName, DataType dataType, string value) : this(recordName, dataType)
            {
                switch (dataType)
                {
                    case DataType.Bool:
                        _boolValue = bool.Parse(value);
                        Value = _boolValue.ToString(); 
                        break;
                    case DataType.Integer:
                        _intValue = float.Parse(value);
                        Value = _intValue.ToString();
                        break;
                    case DataType.Float:
                        _floatValue = float.Parse(value);
                        Value = _floatValue.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
                }
            }
        }
    }
}