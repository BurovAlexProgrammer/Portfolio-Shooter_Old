using System.Collections.Generic;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public class PoolService : BaseService
    {
        private Dictionary<MonoPoolItemBase, MonoPool> _poolDictionary;

        public void Init()
        {
            _poolDictionary = new Dictionary<MonoPoolItemBase, MonoPool>();
        }

        public MonoPoolItemBase GetAndActivate(MonoPoolItemBase prefab)
        {
            var result = Get(prefab);
            result.gameObject.SetActive(true);
            return result;
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

        public void Restart()
        {
            foreach (var item in _poolDictionary)
            {
                item.Value.DeactivateItems();
            }
        }
    }
}