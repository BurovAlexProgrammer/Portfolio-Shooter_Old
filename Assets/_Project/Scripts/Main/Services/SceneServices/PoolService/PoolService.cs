using System;
using System.Collections.Generic;
using Main.Contexts;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;
using UnityEngine;

namespace Main.Services
{
    public class PoolService : IPoolService
    {
        private Dictionary<object, Pool> _poolDictionary = new Dictionary<object, Pool>();
        private Dictionary<UInt64, PoolItem> _itemsDictionary = new Dictionary<UInt64, PoolItem>();
        private Transform _itemsContainer;

        public void Construct()
        {
            var poolService = new GameObject() { name = "Pool Service"};
            poolService.transform.SetParent(Context.ServicesHierarchy);
            _itemsContainer = poolService.transform;
        }
        
        public void Init()
        {
            _poolDictionary = new Dictionary<object, Pool>();
        }

        public PoolItem GetAndActivate(object prefab)
        {
            var poolItem = Get(prefab);
            poolItem.GameObject?.SetActive(true);
            return poolItem;
        }

        public Pool CreatePool(object objectRef, int initialCapacity = 1, int maxCapacity = 20, Pool.OverAllocationBehaviour behaviour = Pool.OverAllocationBehaviour.Warning)
        {
            _poolDictionary ??= new Dictionary<object, Pool>();
            
            if (_poolDictionary.ContainsKey(objectRef))
            {
                return _poolDictionary[objectRef];
            }
            
            Transform poolContainer;

            if (objectRef is GameObject gameObject)
            {
                poolContainer = new GameObject(gameObject.name).transform;
                poolContainer.transform.SetParent(_itemsContainer);
            }
            else if (objectRef is MonoBehaviour monoBehaviour)
            {
                poolContainer = new GameObject(monoBehaviour.gameObject.name).transform;
                poolContainer.transform.SetParent(_itemsContainer);
            }
            else
            {
                poolContainer = _itemsContainer;
            }
            
            var newPool = new Pool(objectRef, poolContainer.transform, initialCapacity, maxCapacity, behaviour);
            _poolDictionary.Add(objectRef, newPool);
            
            return newPool;
        }
        
        public PoolItem Get(object objectKey)
        {
            if (_poolDictionary == null || !_poolDictionary.ContainsKey(objectKey))
            {
                Debug.LogWarning("Pool created automatically by call method PoolService.Get(Prefab).");
                CreatePool(objectKey);
            }

            return _poolDictionary[objectKey].Get();
        }

        public void Reset()
        {
            foreach (var (key, pool) in _poolDictionary)
            {
                pool.DeactivateItems();
            }
        }
        
        public void ReturnItem(UInt64 id)
        {
            foreach (var (key, pool) in _poolDictionary)
            {
                if (pool.ItemsDictionary.TryGetValue(id, out var poolItem))
                {
                    poolItem.ReturnToPool();
                    return;
                }
            }

            throw new Exception($"Pool item with id:\"{id}\" not found.");
        }
    }
}