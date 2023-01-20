using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Extension;
using UnityEngine;

namespace _Project.Scripts.Main
{
    [Serializable]
    public class AnimatorInfo
    {
        public readonly Dictionary<Common.AnimatorParameterNames, AnimatorKey> _keys;

        public AnimatorInfo(Animator animator)
        {
            _keys = new Dictionary<Common.AnimatorParameterNames, AnimatorKey>(animator.parameters.Length);

            var animatorKeyNames = Enum.GetNames(typeof(Common.AnimatorParameterNames));
            foreach (var parameter in animator.parameters)
            {
                if (animatorKeyNames.Contains(parameter.name) == false)
                    throw new Exception($"Common.AnimatorKeys does not contain key '{parameter.name}'.");

                _keys.Add(Enum.Parse<Common.AnimatorParameterNames>(parameter.name), new AnimatorKey(parameter.name, parameter.type, animator));
            }
        }

        public AnimatorKey Get(Common.AnimatorParameterNames key)
        {
            return _keys[key];
        }
        
        
    }
}