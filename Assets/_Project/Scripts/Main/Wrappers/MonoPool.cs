using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Main.Wrappers
{
    [Serializable]
    public class MonoPool<T> where T : MonoPoolItemBase
    {
        private T _prefab;
        private int _initCapacity;
        private int _maxCapacity;
        private Transform _container;
        private OverAllocationBehaviour _overAllocationBehaviour;

        private Queue<T> _inactivePool;
        private List<T> _activePool;

        public enum OverAllocationBehaviour
        {
            Warning,
            ReplaceFirst,
            DestroyFirst,
            DestructFirst
        }

        public MonoPool(T prefab, Transform container, int initialCapacity, int maxCapacity, OverAllocationBehaviour behaviour = OverAllocationBehaviour.Warning)
        {
            _prefab = prefab;
            _container = container;
            _initCapacity = initialCapacity;
            _maxCapacity = maxCapacity;
            _overAllocationBehaviour = behaviour;
            _inactivePool = new Queue<T>();
            _activePool = new List<T>();

            for (var i = 0; i < _initCapacity; i++)
            {
                AddInstance();
            }
        }
        
        public T Get()
        {
            if (_inactivePool.Count == 0)
            {
                AddInstance();
            }
            
            var instance = _inactivePool.Dequeue();
            _activePool.Add(instance);

            return instance;
        }

        private void AddInstance()
        {
            var instance = Object.Instantiate(_prefab, _container);
            instance.gameObject.SetActive(false);
            instance.Returned += OnItemReturned;
            _inactivePool.Enqueue(instance);
        }

        private void OnItemReturned(MonoPoolItemBase item)
        {
            var index = _activePool.FindIndex(x => x.Id == item.Id);
            _activePool.RemoveAt(index);
            _inactivePool.Enqueue(item as T);
        }
    }
}