﻿using System;
using System.Collections.Generic;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.AppServices.Base;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Installers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Game
{
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private Dependencies _dependencies = Dependencies.Siblings;
        [SerializeField] private float _radius = 2;
        [SerializeField] private float _force = 0.2f;
        [SerializeField] private float _liftForce = 0.1f;
        [SerializeField] private ForceMode _forceMode = ForceMode.Force;
        
        [Inject] private DebugService _debugService;

        private Collider[] _colliders;
        private List<Rigidbody> _rigidbodies;

        private void Awake()
        {
            _rigidbodies = new List<Rigidbody>();

            switch (_dependencies)
            {
                case Dependencies.Siblings:
                    _colliders = transform.parent == null
                        ? transform.root.GetComponentsInChildren<Collider>()
                        : transform.parent.GetComponentsInChildren<Collider>();
                    break;
                case Dependencies.Children:
                    _colliders = transform.GetComponentsInChildren<Collider>();
                    break;
                case Dependencies.Colliders:
                    _colliders = Physics.OverlapSphere(transform.position, _radius);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < _colliders.Length; i++)
            {
                var targetCollider = _colliders[i];
                var targetRigidbody = targetCollider.GetComponent<Rigidbody>();

                if (targetRigidbody == null) continue;

                targetRigidbody.WakeUp();
                _rigidbodies.Add(targetRigidbody);
            }
        }

        private async void OnEnable()
        {
            _debugService.CreateExplosionGizmo(transform, _radius);

            for (var i = 0; i < _rigidbodies.Count; i++)
            {
                await UniTask.NextFrame();

                if (_rigidbodies[i] == null) continue;

                _rigidbodies[i].AddExplosionForce(_force, transform.position, _radius, _liftForce,
                    _forceMode);
            }
        }

        private enum Dependencies
        {
            Siblings,
            Children,
            Colliders,
        }
    }
}