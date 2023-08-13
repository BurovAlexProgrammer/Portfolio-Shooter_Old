using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Main.Wrappers
{
    [Serializable]
    public class MonoPool: MonoBehaviour
    {
        private BasePoolItem _prefab;
        private int _initCapacity;
        private int _maxCapacity;
        private int _instanceCount;
        private Transform _transform;
        private OverAllocationBehaviour _overAllocationBehaviour;
        private DiContainer _diContainer;

        private Queue<BasePoolItem> _inactivePool;
        private List<BasePoolItem> _activePool;

        public enum OverAllocationBehaviour
        {
            Warning,
            ReplaceFirst,
            DestroyFirst,
            DestructFirst
        }

        public static MonoPool Instantiate(DiContainer diContainer, Transform parent)
        {
            var instance = new GameObject();
            var monoPool = instance.AddComponent<MonoPool>();
            monoPool.SetDiContainer(diContainer);
            instance.transform.SetParent(parent);
            return monoPool;
        }
        
        public static MonoPool Instantiate(Transform parent)
        {
            var instance = Instantiate(new GameObject(), parent);
            var pool = instance.AddComponent<MonoPool>();
            return pool;
        }

        public void Setup(BasePoolItem prefab, int initialCapacity, int maxCapacity, OverAllocationBehaviour behaviour = OverAllocationBehaviour.Warning)
        {
            Clear();
            _prefab = prefab;
            _transform = transform;
            _initCapacity = initialCapacity;
            _maxCapacity = maxCapacity;
            _overAllocationBehaviour = behaviour;
            
            for (var i = 0; i < _initCapacity; i++)
            {
                AddInstance();
            }
        }

        public void SetDiContainer(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public BasePoolItem Get()
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
                foreach (var item in _inactivePool.ToArray())
                {
                    Object.DestroyImmediate(item.gameObject);
                }
            }

            if (_activePool != null)
            {
                foreach (var item in _activePool.ToArray())
                {
                    Object.DestroyImmediate(item);
                }
            }
            
            _inactivePool = new Queue<BasePoolItem>();
            _activePool = new List<BasePoolItem>();
        }

        public void DeactivateItems()
        {
            if (_activePool != null)
            {
                foreach (var item in _activePool.ToArray())
                {
                    if (item.gameObject.activeSelf == false) continue; 
                        
                    item.ReturnToPool();
                }
            }
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
            
            var instance = _diContainer.InstantiatePrefab(_prefab, _transform).GetComponent<BasePoolItem>();
            instance.gameObject.name = _prefab.name + " " + (_inactivePool.Count + _activePool.Count + 1);
            instance.gameObject.SetActive(false);
            instance.Returned += OnItemReturn;
            _inactivePool.Enqueue(instance);
            _instanceCount++;
        }

        private void OnItemReturn(BasePoolItem item)
        {
            var index = _activePool.FindIndex(x => x.Id == item.Id);
            
            if (index < 0) return;
            
            _activePool.RemoveAt(index);
            _inactivePool.Enqueue(item);
        }
    }
}