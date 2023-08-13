using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Main.Contexts
{
    public class MonoBehaviourExtended : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;

        protected GameObject InstantiateWithInject(GameObject objectTemplate)
        {
            if (objectTemplate is null)
            {
                throw new Exception("GameObject cannot be null");
            }
            
            return _diContainer.InstantiatePrefab(objectTemplate);
        }
        
        protected T InstantiateWithInject<T>(T mono) where T : Object
        {
            if (mono is null)
            {
                throw new Exception("Component cannot be null");
            }
            
            return _diContainer.InstantiatePrefabForComponent<T>(mono);
        }
        
        protected T InstantiateWithInject<T>(T mono, Transform parent) where T : Object
        {
            return _diContainer.InstantiatePrefabForComponent<T>(mono, parent);
        }
    }
}