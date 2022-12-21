using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Extension
{
    public static partial class Common 
    {
        public static T[] Shuffle<T>(this T[] values) where T : struct
        {
            var indexes = new List<int>(values.Length);
            var result = new T[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                indexes.Add(i);
            }

            for (var i = 0; i < values.Length; i++)
            {
                var randomIndex = Random.Range(0, indexes.Count);
                result[i] = values[indexes[randomIndex]];
                indexes.RemoveAt(randomIndex);
            }

            return result;
        }

        public static T[] ShuffleItems<T>(this T[] values) where T : class
        {
            var indexes = new List<int>(values.Length);
            var result = new T[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                indexes.Add(i);
            }

            for (var i = 0; i < values.Length; i++)
            {
                var randomIndex = Random.Range(0, indexes.Count);
                result[i] = values[indexes[randomIndex]];
                indexes.RemoveAt(randomIndex);
            }

            return result;
        }
    }
}