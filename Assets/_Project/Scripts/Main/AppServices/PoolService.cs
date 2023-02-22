using System.Collections.Generic;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public class PoolService : IService
    {
        private GameObject _gameObject;
        private Transform _transform;
        
        public PoolService()
        {
            _poolDictionary = new Dictionary<MonoPoolItemBase, MonoPool>();
            _gameObject = GameObject.Instantiate(new GameObject("Pool Service"));
            _transform = _gameObject.transform;
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