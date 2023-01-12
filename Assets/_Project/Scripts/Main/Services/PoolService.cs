using System.Collections.Generic;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public class PoolService : BaseService
    {
        public Dictionary<MonoPoolItemBase, MonoPool> _poolDictionary;

        public void Init()
        {
            _poolDictionary = new Dictionary<MonoPoolItemBase, MonoPool>();
        }

        public MonoPoolItemBase Get(MonoPoolItemBase prefab)
        {
            if (_poolDictionary.ContainsKey(prefab) == false)
            {
                var newContainer = new GameObject(prefab.name);
                newContainer.transform.SetParent(transform);
                var newPool = new MonoPool(prefab, newContainer.transform, 10, 20);
                _poolDictionary.Add(prefab, newPool);
            }

            return _poolDictionary[prefab].Get();
        }
    }
}