using UnityEngine;
// ReSharper disable InconsistentNaming

namespace _Project.Scripts.Main
{
    public abstract class MonoWrapper : MonoBehaviour
    {
        private GameObject _gameObjectRef;
        private Transform _transformRef;
        public GameObject _gameObject => _gameObjectRef ?? gameObject;
        public Transform _transform => _transformRef ?? transform;

        public virtual void Awake()
        {
            _transformRef = base.transform;
            _gameObjectRef = base.gameObject;
        }
    }
}