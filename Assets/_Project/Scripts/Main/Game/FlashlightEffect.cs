﻿using System;
using System.Collections.Generic;
using System.Threading;
using Main.Extension;
using Main.Extension.Attributes;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Main.Game
{
    public class FlashlightEffect : MonoBehaviour
    { 
        [SerializeField, ReadOnlyField] private List<Light> _lights;
        [SerializeField] private Dependencies _dependencies = Dependencies.CurrentGameObject;
        [SerializeField] private int _flashCount = 3;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Mode _forceMode = Mode.Destroy;

        private Sequence[] _sequences;
        private List<Rigidbody> _rigidbodies;
        private CancellationToken _cancellationToken;
        private float _initIntensity = 1f;

        private void Awake()
        {
            _cancellationToken = gameObject.GetCancellationTokenOnDestroy();
                
            switch (_dependencies)
            {
                case Dependencies.CurrentGameObject:
                    _lights = new List<Light> { GetComponent<Light>() };
                    _initIntensity = _lights[0].intensity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _sequences = new Sequence[_lights.Count];
        }

        private async void OnEnable()
        {
            await UniTask.NextFrame(_cancellationToken);
            
            for (var i = 0; i < _lights.Count; i++)
            {

                if (_lights[i] == null) continue;

                _lights[i].enabled = true;
                
                switch (_forceMode)
                {
                    case Mode.Destroy:
                        RunDestroy(i, _lights[i]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _lights.Count; i++)
            {
                _lights[i].intensity = _initIntensity;
            }
        }

        private void RunDestroy(int sequenceIndex, Light light)
        {
            var period = _duration;
            var firstPeriod = true;
            var sequence = _sequences[sequenceIndex];
            sequence?.Kill();
            sequence = DOTween.Sequence();
            
            for (var i = 0; i < _flashCount; i++)
            {
                period /= 2f;
                
                if (!firstPeriod)
                {
                    sequence.Append(light.DOIntensity(_initIntensity, period / 2));
                }

                sequence.Append(light.DOIntensity(0f, period / 2));
                firstPeriod = false;
            }

            sequence.OnComplete(() => light.enabled = false);
            sequence.WithCancellation(_cancellationToken);
            sequence.Play();
        }

        private enum Dependencies
        {
            CurrentGameObject
        }

        private enum Mode
        {
            Destroy
        }
    }
}