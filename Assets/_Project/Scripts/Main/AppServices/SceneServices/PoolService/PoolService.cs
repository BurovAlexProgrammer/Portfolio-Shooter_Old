using System;
using System.Collections.Generic;
using _Project.Scripts.Main.Contexts;
using _Project.Scripts.Main.Wrappers;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Main.AppServices.SceneServices.PoolService
{
    [UsedImplicitly]
    public class PoolService : IPoolService
    {
        [Inject] private DiContainer _diContainer;

        private readonly Transform _transform;

        public PoolService()
        {
            _poolDictionary = new Dictionary<BasePoolItem, MonoPool>();
            _transform = Object.Instantiate(new GameObject("Pool Service")).transform;
            this.RegisterContext();
        }

        private Dictionary<BasePoolItem, MonoPool> _poolDictionary;

        public BasePoolItem GetAndActivate(BasePoolItem prefab)
        {
            var result = Get(prefab);
            result.gameObject.SetActive(true);
            return result;
        }

        public BasePoolItem Get(BasePoolItem prefab)
        {
            if (_poolDictionary.ContainsKey(prefab) == false)
            {
                var pool = MonoPool.Instantiate(_diContainer, _transform);
                pool.transform.name = prefab.name;
                pool.Setup(prefab, 1, 100);
                _poolDictionary.Add(prefab, pool);
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

        protected GameObject InstantiateWithInject(GameObject objectTemplate)
        {
            if (objectTemplate is null)
            {
                throw new Exception("GameObject cannot be null");
            }

            return _diContainer.InstantiatePrefab(objectTemplate);
        }
    }
}