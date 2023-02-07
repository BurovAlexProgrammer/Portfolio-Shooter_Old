using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Main
{
    public abstract class MonoBeh : MonoBehaviour
    {
        private GameObject _gameObjectRef;
        private Transform _transformRef;
        private CancellationToken _destroyCancellationToken;
        private bool _isDestroyed;

        public GameObject GameObject => !IsDestroyed
            ? _gameObjectRef ?? gameObject
            : throw new Exception("GameObject is already destroyed.");

        public Transform Transform => !IsDestroyed
            ? _transformRef ?? transform
            : throw new Exception("Transform is already destroyed.");

        public CancellationToken DestroyCancellationToken => _destroyCancellationToken;
        public bool IsDestroyed => _destroyCancellationToken.IsCancellationRequested;
        public bool IsAvailable => GameObject != null && GameObject.activeSelf && !IsDestroyed;
        protected GameObject _gameObject => GameObject;
        protected Transform _transform => Transform;

        protected virtual void OnAwake()
        {
        }

        private void Awake()
        {
            _destroyCancellationToken = gameObject.GetCancellationTokenOnDestroy();
            _gameObjectRef = gameObject;
            _transformRef = transform;
            OnAwake();
        }
    }
}