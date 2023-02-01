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
        private bool _isInitialized;

        public GameObject GameObject => _isInitialized ? _gameObjectRef : gameObject;
        public Transform Transform => _isInitialized ? _transformRef : transform;
        public CancellationToken DestroyCancellationToken => _destroyCancellationToken;
        public bool IsDestroyed => _destroyCancellationToken.IsCancellationRequested;
        public bool IsAvailable => GameObject != null && GameObject.activeSelf && !IsDestroyed;
        protected GameObject _gameObject => GameObject;
        protected Transform _transform => Transform;

        protected MonoBeh()
        {
            Init();
        }

        private async void Init()
        { 
            await UniTask.Yield();

            _isInitialized = true;

            try
            {
                _destroyCancellationToken = gameObject.GetCancellationTokenOnDestroy();
                _gameObjectRef = gameObject;
                _transformRef = transform;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}