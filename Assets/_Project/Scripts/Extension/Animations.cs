using System;
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
            for (var i = 0; i < animator.parameters.Length; i++)
            {
                var parameter = animator.parameters[i];
                if (Enum.GetNames(typeof(AnimatorParameterNames)).Contains(parameter.name)) continue;
                throw new Exception($"Common.AnimatorKeys does not contain key '{parameter.name}'.");
            }
        }

        public static AnimatorControllerParameter GetParameter(this Animator animator, AnimatorParameterNames key)
        {
            return animator.parameters.Single(x => x.name.Equals(key.ToString()));
        }

        public static void SetTrigger(this Animator animator, AnimatorParameterNames parameterName)
        {
            var parameter = animator.GetParameter(parameterName);
            
            if (parameter.type != AnimatorControllerParameterType.Trigger) 
                throw new Exception(ExceptionMessages.AnimatorParameterInvalidType(parameter));

            animator.SetTrigger(AnimatorParameters[parameterName]);
        }
        
        public static void SetFloat(this Animator animator, AnimatorParameterNames parameterName, float value)
        {
            var parameter = animator.GetParameter(parameterName);
            
            if (parameter.type != AnimatorControllerParameterType.Float) 
                throw new Exception(ExceptionMessages.AnimatorParameterInvalidType(parameter));
            
            animator.SetFloat(AnimatorParameters[parameterName], value);
        }
        
        public static void SetInt(this Animator animator, AnimatorParameterNames parameterName, int value)
        {
            var parameter = animator.GetParameter(parameterName);
            
            if (parameter.type != AnimatorControllerParameterType.Int) 
                throw new Exception(ExceptionMessages.AnimatorParameterInvalidType(parameter));
            
            animator.SetInteger(AnimatorParameters[parameterName], value);
        }
        
        public static void SetBool(this Animator animator, AnimatorParameterNames parameterName, bool value)
        {
            var parameter = animator.GetParameter(parameterName);
            
            if (parameter.type != AnimatorControllerParameterType.Bool) 
                throw new Exception();

            animator.SetBool(AnimatorParameters[parameterName], value);
        }

        public static float GetClipLength(this Animator animator, int layerIndex)
        {
            return animator.GetCurrentAnimatorClipInfo(layerIndex).Length;
        }

        public static class ExceptionMessages
        {
            public static string AnimatorParameterInvalidType(AnimatorControllerParameter parameter) =>
                $"AnimatorParameter '{parameter.name}' is not Bool type. (Current type: {parameter.type.ToString()})";
        }
    }
}