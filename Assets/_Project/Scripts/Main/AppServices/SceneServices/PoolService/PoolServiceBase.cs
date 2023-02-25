using System.Collections.Generic;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices.SceneServices.PoolService
{
    public abstract class PoolServiceBase : IPoolService
    {
        private readonly Transform _transform;
        
        public PoolServiceBase()
        {
            _poolDictionary = new Dictionary<MonoPoolItemBase, MonoPool>();
            _transform = Object.Instantiate(new GameObject("Pool Service")).transform;
            this.RegisterContext();
        }
        
        private Dictionary<MonoPoolItemBase, MonoPool> _poolDictionary;
        
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
                newContainer.transform.SetParent(_transform);
                var newPool = new MonoPool(prefab, newContainer.transform, 1, 100);
                _poolDictionary.Add(prefab, newPool);
            }

            return _poolDictionary[prefab].Get();
        }

        public void Disable()
        {
            foreach (var item in _poolDictionary)
            {
                item.Value.DeactivateItems();
            }
        }
    }
}