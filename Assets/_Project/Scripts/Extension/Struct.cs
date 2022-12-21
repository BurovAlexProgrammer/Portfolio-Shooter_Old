using UnityEngine;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static int RoundInt(this float value) => Mathf.RoundToInt(value);

        public static int MultiplyInt(this float value, int multi) => Mathf.RoundToInt(value * multi);

        public static int ToMillisecs(this float value) => Mathf.RoundToInt(value * 1000);

        public static void Set(this ref Vector3 vector3, float x = float.NaN, float y = float.NaN, float z = float.NaN)
        {
            if (float.IsNaN(x) == false) vector3.x = x;
            if (float.IsNaN(y) == false) vector3.y = y;
            if (float.IsNaN(z) == false) vector3.z = z;
        }

        public static Color Set(this Color color, float r = float.NaN, float g= float.NaN, float b = float.NaN, float a= float.NaN)
        {
            if (float.IsNaN(r) == false) color.r = r;
            if (float.IsNaN(g) == false) color.g = g;
            if (float.IsNaN(b) == false) color.b = b;
            if (float.IsNaN(a) == false) color.a = a;
            return color;
        }
    }
}