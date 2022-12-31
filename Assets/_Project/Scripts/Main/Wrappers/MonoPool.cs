using System;
using System.Collections.Generic;
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
        private int _instanceCount;
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
            Clear();
            _prefab = prefab;
            _container = container;
            _initCapacity = initialCapacity;
            _maxCapacity = maxCapacity;
            _overAllocationBehaviour = behaviour;

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

        public void Clear()
        {
            _instanceCount = 0;

            if (_inactivePool != null)
            {
                foreach (var item in _inactivePool)
                {
                    Object.DestroyImmediate(item.gameObject);
                }
            }

            if (_activePool != null)
            {
                foreach (var item in _activePool)
                {
                    Object.DestroyImmediate(item);
                }
            }
            
            _inactivePool = new Queue<T>();
            _activePool = new List<T>();
        }

        private void AddInstance()
        {
            if (_instanceCount == _maxCapacity)
            {
                switch (_overAllocationBehaviour)
                {
                    case OverAllocationBehaviour.Warning:
                        Debug.LogWarning($"Pool of '{_prefab.name}' is over allocated.");
                        break;
                    case OverAllocationBehaviour.ReplaceFirst:
                        break;
                    case OverAllocationBehaviour.DestroyFirst:
                        break;
                    case OverAllocationBehaviour.DestructFirst:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            var instance = Object.Instantiate(_prefab, _container);
            instance.gameObject.SetActive(false);
            instance.Returned += OnItemReturn;
            _inactivePool.Enqueue(instance);
            _instanceCount++;
        }

        private void OnItemReturn(MonoPoolItemBase item)
        {
            var index = _activePool.FindIndex(x => x.Id == item.Id);
            
            if (index < 0) return;
            
            _activePool.RemoveAt(index);
            _inactivePool.Enqueue(item as T);
        }
    }
}