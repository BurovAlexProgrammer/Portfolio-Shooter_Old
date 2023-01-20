using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static readonly Dictionary<AnimatorParameterNames, string> AnimatorParameters = Enum.GetNames(typeof(AnimatorParameterNames))
            .ToDictionary(Enum.Parse<AnimatorParameterNames>);

        public enum AnimatorParameterNames
        {
            Velocity,
            Attack,
        }

        public static void ValidateParameters(this Animator animator)
        {
            foreach (var parameter in animator.parameters)
            {
                if (Enum.GetNames(typeof(AnimatorParameterNames)).Contains(parameter.name)) continue;
                throw new Exception($"Common.AnimatorKeys does not contain key '{parameter.name}'.");
            }
        }

        public static AnimatorControllerParameter GetParameter(this Animator animator, AnimatorParameterNames key)
        {
            return animator.parameters.Single(x => x.name.Equals(key.ToString()));
        }
    }
}