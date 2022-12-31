using UnityEngine;
// ReSharper disable InconsistentNaming

namespace _Project.Scripts.Main
{
    public abstract class MonoBeh : MonoBehaviour
    {
        private GameObject _gameObjectRef;
        private Transform _transformRef;
        public GameObject _gameObject
        {
            get
            {
                _gameObjectRef ??= gameObject;
                return _gameObjectRef;
            }
        }

        public Transform _transform
        {
            get
            {
                _transformRef ??= transform;
                return _transformRef;
            }
        }
    }
}