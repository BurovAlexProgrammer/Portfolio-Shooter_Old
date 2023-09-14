using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Main.Extension.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Extension
{
    public static partial class Common
    {
        public static int RoundInt(this float value) => Mathf.RoundToInt(value);

        public static int MultiplyInt(this float value, int multi) => Mathf.RoundToInt(value * multi);

        public static int ToMillisecs(this float value) => Mathf.RoundToInt(value * 1000);

        public static Vector3 SetAsNew(this Vector3 vector3, float x = float.NaN, float y = float.NaN, float z = float.NaN)
        {
            var result = vector3;
            if (float.IsNaN(x) == false) result.x = x;
            if (float.IsNaN(y) == false) result.y = y;
            if (float.IsNaN(z) == false) result.z = z;
            return result;
        }
        
        public static void Set(this ref Vector3 vector3, float x = float.NaN, float y = float.NaN, float z = float.NaN)
        {
            if (float.IsNaN(x) == false) vector3.x = x;
            if (float.IsNaN(y) == false) vector3.y = y;
            if (float.IsNaN(z) == false) vector3.z = z;
        }
        
        public static void Set(this ref Quaternion quaternion, float x = float.NaN, float y = float.NaN, float z = float.NaN, float w = float.NaN)
        {
            if (float.IsNaN(x) == false) quaternion.x = x;
            if (float.IsNaN(y) == false) quaternion.y = y;
            if (float.IsNaN(z) == false) quaternion.z = z;
            if (float.IsNaN(w) == false) quaternion.w = w;
        }
        
        public static void New(this Quaternion quaternion, float x = float.NaN, float y = float.NaN, float z = float.NaN, float w = float.NaN)
        {
            if (float.IsNaN(x) == false) quaternion.x = x;
            if (float.IsNaN(y) == false) quaternion.y = y;
            if (float.IsNaN(z) == false) quaternion.z = z;
            if (float.IsNaN(w) == false) quaternion.w = w;
        }

        public static Color Set(this ref Color color, float r = float.NaN, float g= float.NaN, float b = float.NaN, float a= float.NaN)
        {
            if (float.IsNaN(r) == false) color.r = r;
            if (float.IsNaN(g) == false) color.g = g;
            if (float.IsNaN(b) == false) color.b = b;
            if (float.IsNaN(a) == false) color.a = a;
            return color;
        }
        
        public static Color New(this Color color, float r = float.NaN, float g= float.NaN, float b = float.NaN, float a= float.NaN)
        {
            if (float.IsNaN(r) == false) color.r = r;
            if (float.IsNaN(g) == false) color.g = g;
            if (float.IsNaN(b) == false) color.b = b;
            if (float.IsNaN(a) == false) color.a = a;
            return color;
        }

        public static async UniTask WaitInSeconds(this float time, PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), DelayType.DeltaTime, playerLoopTiming, cancellationToken);
        }
        
        public static async UniTask WaitInSeconds(this float time, CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), DelayType.DeltaTime, PlayerLoopTiming.Update, cancellationToken);
        }

        public static float GetRandomValue(this RangedFloat rangedFloat)
        {
            return Random.Range(rangedFloat.MinValue, rangedFloat.MaxValue);
        }
    }
}