using System;

namespace Main.Extension.Attributes
{
    [Serializable]
    public struct RangedFloat
    {
        public float MinValue;
        public float MaxValue;

        public RangedFloat(float min, float max)
        {
            MinValue = min;
            MaxValue = max;
        }
    }
}