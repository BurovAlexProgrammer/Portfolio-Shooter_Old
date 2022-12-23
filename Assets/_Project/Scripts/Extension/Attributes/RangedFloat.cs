using System;

namespace _Project.Scripts.Extension.Attributes
{
    [Serializable]
    public struct RangedFloat
    {
        private float _minValue;
        private float _maxValue;
    }

    public class MinMaxRangeAttribute : Attribute
    {
        public MinMaxRangeAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min { get; private set; }
        public float Max { get; private set; }
    }
}