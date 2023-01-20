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

        public static void SetTrigger(this Animator animator, AnimatorParameterNames name)
        {
            var parameter = animator.GetParameter(name);
            
            if (parameter.type != AnimatorControllerParameterType.Trigger) 
                throw new Exception(ExceptionMessages.AnimatorParameterInvalidType(parameter));
            
            animator.SetTrigger(AnimatorParameters[AnimatorParameterNames.Attack]);
        }
        
        public static void SetFloat(this Animator animator, AnimatorParameterNames name, float value)
        {
            var parameter = animator.GetParameter(name);
            
            if (parameter.type != AnimatorControllerParameterType.Trigger) 
                throw new Exception(ExceptionMessages.AnimatorParameterInvalidType(parameter));
            
            animator.SetFloat(AnimatorParameters[AnimatorParameterNames.Attack], value);
        }
        
        public static void SetInt(this Animator animator, AnimatorParameterNames name, int value)
        {
            var parameter = animator.GetParameter(name);
            
            if (parameter.type != AnimatorControllerParameterType.Trigger) 
                throw new Exception(ExceptionMessages.AnimatorParameterInvalidType(parameter));
            
            animator.SetInteger(AnimatorParameters[AnimatorParameterNames.Attack], value);
        }
        
        public static void SetBool(this Animator animator, AnimatorParameterNames name, bool value)
        {
            var parameter = animator.GetParameter(name);
            
            if (parameter.type != AnimatorControllerParameterType.Trigger) 
                throw new Exception();

            animator.SetBool(AnimatorParameters[AnimatorParameterNames.Attack], value);
        }

        public static class ExceptionMessages
        {
            public static string AnimatorParameterInvalidType(AnimatorControllerParameter parameter) =>
                $"AnimatorParameter '{parameter.name}' is not Bool type. (Current type: {parameter.type.ToString()})";
        }
    }
}