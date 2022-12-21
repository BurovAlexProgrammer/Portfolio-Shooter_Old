using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static float GetDuration(this AnimationCurve curve)
        {
            return curve[curve.keys.Length - 1].time;
        }

        public static Sequence Reset(this Sequence sequence, bool complete = true)
        {
            sequence?.Kill(complete);
            return DOTween.Sequence();
        }

        public static async UniTask Wait(float seconds)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        }
    }
}