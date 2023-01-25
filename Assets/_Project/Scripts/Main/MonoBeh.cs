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

        public GameObject _gameObject => _gameObjectRef ?? gameObject;
        public Transform _transform => _transformRef ?? transform;
        public CancellationToken DestroyCancellationToken => _destroyCancellationToken;
        public bool Destroyed => _destroyCancellationToken.IsCancellationRequested;
        public bool Available => _gameObject != null && _gameObject.activeSelf && !Destroyed;

        protected MonoBeh()
        {
            Init();
        }

        async void Init()
        {
            await UniTask.Yield();
            _destroyCancellationToken = gameObject.GetCancellationTokenOnDestroy();
            _gameObjectRef = gameObject;
            _transformRef = transform;
        }
    }
}