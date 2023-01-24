using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Main.Game.Weapon
{
    public class Destruction : MonoPoolItemBase
    {
        [SerializeField] private Behaviors _behavior;
        [SerializeField] private float _lifeTime = 5f;

        private Transform[] _childrenTransforms;
        private TransformInfo[] _childrenInitTransformInfos;

        private void Awake()
        {
            _childrenTransforms = _transform.GetChildrenTransforms();
            _childrenInitTransformInfos = _transform.GetChildrenTransformInfo();
        }

        private void OnEnable()
        {
            RestoreTransforms();
            
            switch (_behavior)
            {
                case Behaviors.ReturnToPool:
                    ReturnToPoolByLifetime();
                    break;
                case Behaviors.FadeAlpha:
                    throw new NotImplementedException();
                case Behaviors.FadeScaleChildren:
                    FadeScaleChildren();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RestoreTransforms()
        {
            for (var i = 0; i < _childrenTransforms.Length; i++)
            {
                var childTransform = _childrenTransforms[i];
                var childInitTransformInfo = _childrenInitTransformInfos[i];
                childTransform.localPosition = childInitTransformInfo.LocalPosition;
                childTransform.localRotation = childInitTransformInfo.LocalRotation;
                childTransform.localScale = childInitTransformInfo.LocalScale;
            }
        }

        private async void ReturnToPoolByLifetime()
        {
            await _lifeTime.WaitInSeconds();
            ReturnToPool();
        }

        private async void FadeScaleChildren()
        {
            await (_lifeTime / 2f).WaitInSeconds();
            var children = _transform.GetChildrenTransforms();
            
            for (var i = 0; i < children.Length; i++) 
            {
                children[i].DOScale(0f, _lifeTime / 2f * Random.Range(0.5f, 1f));
            }

            await (_lifeTime / 2f).WaitInSeconds();
            
            for (var i = 0; i < children.Length; i++)
            {
                children[i].DOComplete();
                children[i].localScale = Vector3.one;
            }

            ReturnToPool();
        }

        private enum Behaviors
        {
            ReturnToPool,
            FadeAlpha,
            FadeScaleChildren,
        }
    }
}