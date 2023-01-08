using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Main.Wrappers;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    public class Destruction : MonoPoolItemBase
    {
        [SerializeField] private Behaviors _behavior;
        [SerializeField] private float _lifeTime = 5f;

        private void Start()
        {
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

        private async void ReturnToPoolByLifetime()
        {
            await _lifeTime.WaitInSeconds();
            ReturnToPool();
        }

        private async void FadeScaleChildren()
        {
            await (_lifeTime / 2f).WaitInSeconds();

            foreach (Transform child in _transform)
            {
                child.DOScale(0f, _lifeTime / 2f);
            }

            await (_lifeTime / 2f).WaitInSeconds();
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