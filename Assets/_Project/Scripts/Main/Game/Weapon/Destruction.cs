using System;
using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Game.Weapon
{
    public class Destruction : MonoBeh
    {
        [SerializeField] private Behaviors _behavior;
        [SerializeField] private float _lifeTime = 5f;

        private void Start()
        {
            switch (_behavior)
            {
                case Behaviors.Destroy:
                    Destroy();
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

        private async void Destroy()
        {
            await _lifeTime.WaitInSeconds();

            if (gameObject.IsDestroyed()) return;

            Destroy(gameObject);
        }

        private async void FadeScaleChildren()
        {
            await (_lifeTime / 2f).WaitInSeconds();
            var rigidbodies = _transform.GetComponentsInChildren<Rigidbody>();

            foreach (var rb in rigidbodies)
            {
                rb.Sleep();
            }

            foreach (Transform child in _transform)
            {
                child.DOScale(0f, _lifeTime / 2f);
            }

            await (_lifeTime / 2f).WaitInSeconds();

            if (gameObject.IsDestroyed()) return;

            Destroy(gameObject);
        }

        private enum Behaviors
        {
            Destroy,
            FadeAlpha,
            FadeScaleChildren,
        }
    }
}