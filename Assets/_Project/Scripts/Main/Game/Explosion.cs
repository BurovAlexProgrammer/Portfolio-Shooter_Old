using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private Dependencies _dependencies = Dependencies.Siblings;
        [SerializeField] private float _radius = 2;
        [SerializeField] private float _force = 0.2f;
        [SerializeField] private float _liftForce = 0.1f;
        [SerializeField] private ForceMode _forceMode = ForceMode.Force;

        private Collider[] _colliders;
        private List<Rigidbody> _rigidbodies;

        private void Awake()
        {
            _rigidbodies = new List<Rigidbody>();
            DebugService.CreateExplosionGizmo(transform, _radius);

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
            
            foreach (var targetCollider in _colliders)
            {
                var targetRigidbody = targetCollider.GetComponent<Rigidbody>();

                if (targetRigidbody == null) continue;
                
                targetRigidbody.WakeUp();
                _rigidbodies.Add(targetRigidbody);
            }
        }

        private async void Start()
        {
            foreach (var targetRigidbody in _rigidbodies)
            {
                await UniTask.NextFrame();
                
                if (targetRigidbody == null) continue;
                
                targetRigidbody.AddExplosionForce(_force, transform.position, _radius, _liftForce,
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