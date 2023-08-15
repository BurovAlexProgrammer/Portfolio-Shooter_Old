using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace sm_application.Scripts.Main.Wrappers
{
    [Serializable]
    public class Pool
    {
        public Dictionary<UInt64, PoolItem> ItemsDictionary = new Dictionary<ulong, PoolItem>();
        
        private object _originRef;
        private int _initCapacity;
        private int _maxCapacity;
        private int _instanceCount;
        private Transform _container;
        private OverAllocationBehaviour _overAllocationBehaviour;
        private Queue<PoolItem> _inactivePool;
        private List<PoolItem> _activePool;

        public enum OverAllocationBehaviour
        {
            Warning,
            ReplaceFirst,
            DestroyFirst,
            DestructFirst
        }

        public Pool(object originRef, Transform container, int initialCapacity, int maxCapacity, OverAllocationBehaviour behaviour = OverAllocationBehaviour.Warning)
        {
            _originRef = originRef;
            _container = container;
            _initCapacity = initialCapacity;
            _maxCapacity = maxCapacity;
            _overAllocationBehaviour = behaviour;
            _inactivePool = new Queue<PoolItem>(_initCapacity);
            _activePool = new List<PoolItem>(_initCapacity);
            
            for (var i = 0; i < _initCapacity; i++)
            {
                AddInstance();
            }
        }
        
        public PoolItem Get()
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
                    ItemsDictionary.Remove(item.Id);
                    item.Destroy();
                }
            }

            if (_activePool != null)
            {
                foreach (var item in _activePool.ToArray())
                {
                    ItemsDictionary.Remove(item.Id);
                    item.Destroy();
                }
            }
            
            _inactivePool = new Queue<PoolItem>();
            _activePool = new List<PoolItem>();
        }

        public void DeactivateItems()
        {
            if (_activePool != null)
            {
                foreach (var item in _activePool.ToArray())
                {
                    if (item.GameObject == null) continue; 
                    if (item.GameObject.activeSelf == false) continue; 
                        
                    item.GameObject.SetActive(false);
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
                        Debug.LogWarning($"Pool of '{GetName(_originRef)}' is over allocated.");
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

            object newInstance;
            var nextIndex = (_inactivePool.Count + _activePool.Count + 1);
            
            switch (_originRef)
            {
                case GameObject gameObject:
                    newInstance = GameObject.Instantiate(gameObject, _container);
                    break;
                case MonoBehaviour monoBehaviour:
                    newInstance = GameObject.Instantiate(monoBehaviour, _container);
                    break;
                default:
                    var originType = _originRef.GetType();
                    newInstance = Activator.CreateInstance(originType);
                    break;
            }
            
            var newItem = new PoolItem(newInstance, nextIndex);
            newItem.OnReturn += OnItemReturn;
            _inactivePool.Enqueue(newItem);
            ItemsDictionary.Add(newItem.Id, newItem);
            _instanceCount++;
        }

        private void OnItemReturn(PoolItem item)
        {
            var index = _activePool.FindIndex(x => x == item);
            
            if (index < 0) return;
            
            _activePool.RemoveAt(index);
            _inactivePool.Enqueue(item);
        }

        private string GetName(object obj)
        {
            return 
                _originRef is GameObject ? (_originRef as GameObject).name :
                _originRef is MonoBehaviour ? (_originRef as MonoBehaviour).gameObject.name :
                _originRef.GetType().Name;
        }
    }
}