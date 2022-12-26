using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Main.Wrappers
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _initCapacity;
        [SerializeField] private int _maxCapacity = 50;
        [SerializeField] private Transform _parent;

        private Queue<GameObject> _pool;

        private void Awake()
        {
            _pool = new Queue<GameObject>();

            for (var i = 0; i < _initCapacity; i++)
            {
                AddInstance();
            }
        }

        public GameObject Get()
        {
            if (_pool.Count == 0)
            {
                AddInstance();
            }
            
            var instance = _pool.Dequeue();

            return instance;
        }

        public void Return(GameObject poolItem)
        {
            poolItem.SetActive(false);
            _pool.Enqueue(poolItem);
        }

        private void AddInstance()
        {
            var instance = Instantiate(_prefab, _parent);
            instance.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
    
        public class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initCapacity;
        [SerializeField] private int _maxCapacity = 50;
        [SerializeField] private Transform _parent;

        private Queue<T> _pool;

        private void Awake()
        {
            _pool = new Queue<T>();

            for (var i = 0; i < _initCapacity; i++)
            {
                AddInstance();
            }
        }

        public T Get()
        {
            if (_pool.Count == 0)
            {
                AddInstance();
            }
            
            var instance = _pool.Dequeue();

            return instance;
        }

        public void Return(T poolItem)
        {
            poolItem.gameObject.SetActive(false);
            _pool.Enqueue(poolItem);
        }

        private void AddInstance()
        {
            var instance = Instantiate(_prefab, _parent);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }
}