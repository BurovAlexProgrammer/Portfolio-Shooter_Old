using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using _Project.Scripts.Main.AppServices;
using Main.Service;
using UnityEngine;
using Object = System.Object;

namespace Main.Contexts
{
    public class Context
    {
        private static readonly Dictionary<Type, IService> _registeredServices = new();
        private static readonly ConditionalWeakTable<Type, Object> _sceneObjects = new();
        private static Transform _contextHierarchy;
        private static Transform _servicesHierarchy;

        public static Transform ContextHierarchy => _contextHierarchy;

        public static Transform ServicesHierarchy => _servicesHierarchy;

        public static void Init(Transform contextHierarchy, Transform servicesHierarchy)
        {
            _contextHierarchy = contextHierarchy;
            _servicesHierarchy = servicesHierarchy;
        }
        
        public static void RegisterService<T>() where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)))
            {
                throw new Exception($"Service type of {typeof(T).Name} registered already");
            }

            var newService = Activator.CreateInstance<T>();

            if (newService is IConstruct)
            {
                (newService as IConstruct).Construct();
            }

            if (newService is IConstructInstaller)
            {
                throw new Exception($"Service {typeof(T).Name} has Construct. Use Services.RegisterService(IServiceInstaller installer) instead");
            }

            _registeredServices.Add(typeof(T), newService);
        }

        public static void RegisterService<T>(IServiceInstaller installer) where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)))
            {
                throw new Exception($"Service type of {typeof(T).Name} registered already");
            }

            var newService = Activator.CreateInstance<T>();

            if (newService is not IConstructInstaller)
            {
                throw new Exception($"Service {typeof(T).Name} doesn't have Construct. Use Services.RegisterService() instead");
            }

            (newService as IConstructInstaller).Construct(installer);
            _registeredServices.Add(typeof(T), newService);
        }

        public static T GetService<T>() where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)) == false)
            {
                throw new Exception($"Service type of {typeof(T).Name} not found.");
            }

            return (T)_registeredServices[typeof(T)];
        }

        public static void InitScene()
        {
            _sceneObjects.Clear();
        }

        public static void RegisterSceneObject(Object obj)
        {
            _sceneObjects.AddOrUpdate(obj.GetType(), obj);
        }

        // public static void RegisterSceneObject(GameObject gameObject)
        // {
        //     _sceneObjects.AddOrUpdate(gameObject, gameObject.GetType());
        // }
        //
        // public static void RegisterSceneObject(MonoBehaviour monoBehaviour)
        // {
        //     _sceneObjects.AddOrUpdate(monoBehaviour, monoBehaviour.GetType());
        // }

        public static T GetSceneObject<T>() where T : class
        {
            var type = typeof(T);

            return _sceneObjects.TryGetValue(type, out var result) ? result as T : null;
        }

        public static void DisposeSceneContext()
        {
            _sceneObjects.Clear();
        }

        public static void Dispose()
        {
            foreach (var type in _registeredServices.Keys.ToList())
            {
                if (_registeredServices[type] is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _registeredServices[type] = null;
            }

            _registeredServices.Clear();
        }
    }
}