
using System;
using UnityEngine;

namespace _Project.Scripts.Main
{
    public class AnimatorKey
    {
        public readonly string Name;
        public readonly AnimatorControllerParameterType Type;

        private Animator _animator;
        private bool _boolValue;
        private int _intValue;
        private float _floatValue;

        public AnimatorKey(string name, AnimatorControllerParameterType type, Animator animator)
        {
            Name = name;
            Type = type;
            _animator = animator;
        }

        public void Set(bool value)
        {
            if (Type != AnimatorControllerParameterType.Bool) 
                throw new Exception($"AnimatorKey '{Name}' is not bool type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");
            
            _animator.SetBool(Name, value);
        }

        public void Set(float value)
        {
            if (Type != AnimatorControllerParameterType.Float) 
                throw new Exception($"AnimatorKey '{Name}' is not float type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");
            
            _animator.SetFloat(Name, value);
        }

        public void Set(int value)
        {
            if (Type != AnimatorControllerParameterType.Int) 
                throw new Exception($"AnimatorKey '{Name}' is not float type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");
            
            _animator.SetInteger(Name, value);
        }

        public void SetTrigger()
        {
            if (Type != AnimatorControllerParameterType.Trigger) 
                throw new Exception($"AnimatorKey '{Name}' is not trigger type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");
            
            _animator.SetTrigger(Name);
        }

        public bool GetBool()
        {
            if (Type != AnimatorControllerParameterType.Bool) 
                throw new Exception($"AnimatorKey '{Name}' is not bool type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");

            return _animator.GetBool(Name);
        }
        
        public int GetInt()
        {
            if (Type != AnimatorControllerParameterType.Bool) 
                throw new Exception($"AnimatorKey '{Name}' is not bool type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");

            return _animator.GetInteger(Name);
        }
        
        public float GetFloat()
        {
            if (Type != AnimatorControllerParameterType.Bool) 
                throw new Exception($"AnimatorKey '{Name}' is not bool type. (Current type: {Enum.GetName(typeof(AnimatorControllerParameterType), Type)})");

            return _animator.GetFloat(Name);
        }
    }
    
}