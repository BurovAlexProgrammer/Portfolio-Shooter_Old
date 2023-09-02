using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Main.Contexts.DI;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;
using UnityEngine;
using Object = System.Object;

namespace Main.Contexts
{
    public class Context
    {
        private static readonly Dictionary<Type, ServiceContainer> _registeredServices = new();
        private static readonly ConditionalWeakTable<Type, SceneContainer> _sceneObjects = new();
        private static Transform _contextHierarchy;
        private static Transform _servicesHierarchy;
        private static Queue<Type> _complexServices = new Queue<Type>();

        public static Transform ContextHierarchy => _contextHierarchy;
        public static Transform ServicesHierarchy => _servicesHierarchy;

        public static void Init(Transform contextHierarchy, Transform servicesHierarchy)
        {
            _contextHierarchy = contextHierarchy;
            _servicesHierarchy = servicesHierarchy;
        }

        public static ServiceContainer BindService<T>(ServiceContainer.Scope scope = ServiceContainer.Scope.App) where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)))
            {
                Log.Error($"Service type of {typeof(T).Name} registered already");
                return null;
            }

            var serviceContainer = new ServiceContainer(typeof(T));

            if (serviceContainer.BindType.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var gameObject = new GameObject();
                GameObject.Instantiate(gameObject, _servicesHierarchy);
                var service = gameObject.AddComponent(serviceContainer.BindType) as IService;
                
                serviceContainer.Service = service;
            }
            else
            {
                serviceContainer.Service = Activator.CreateInstance<T>();
            }

            serviceContainer.Instance = serviceContainer.Service;
            SetDependencies(serviceContainer);
            _registeredServices.Add(typeof(T), serviceContainer);

            return serviceContainer;

            // _registeredServices.Add(typeof(T), serviceContainer);
            // _complexServices.Enqueue(typeof(T));
            //
            // {
            //     for (var i = 0; i < _complexServices.ToArray().Length; i++)
            //     {
            //         try
            //         {
            //             var serviceType = _complexServices.Dequeue();
            //             var service = _registeredServices[serviceType];
            //         }
            //         catch (Exception exc)
            //         {
            //             _complexServices.Enqueue(typeof(T));
            //         }
            //     }
            // }
        }

        public static ServiceContainer BindService<T>(IServiceInstaller installer, ServiceContainer.Scope scope = ServiceContainer.Scope.App) where T : IService
        {
            var serviceContainer = BindService<T>(scope);
            serviceContainer.ConstructInstaller = installer;

            return serviceContainer;
        }

        // private static T InstantiateService<T>() where T : class
        // {
        //     if (typeof(T) is MonoBehaviour)
        //     {
        //         var gameObject = new GameObject();
        //         gameObject.name = typeof(T).Name;
        //         return GameObject.Instantiate(gameObject) as T;
        //     }
        // }

        public static void SetDependencies(ContextContainerBase container)
        {
            var t1 = container.SourceType;
            var fields = container.SourceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                foreach (var attribute in field.GetCustomAttributes(false))
                {
                    if (attribute is InjectAttribute)
                    {
                        var dependencyType = field.FieldType;
                        var dependencyContainer = GetDependency(dependencyType);
                        container.Dependencies.Add(dependencyType);
                        field.SetValue(container.Instance, dependencyContainer.Instance);

                        if (container is ServiceContainer && !IsBoundService(dependencyType))
                        {
                            _complexServices.Enqueue(dependencyType);
                        }
                    }
                }
            }
        }

        public static bool IsBoundService<T>() where T : IService
        {
            return _registeredServices.ContainsKey(typeof(T));
        }

        public static bool IsBoundService(Type type)
        {
            return _registeredServices.ContainsKey(type);
        }

        public static T GetService<T>() where T : IService
        {
            if (_registeredServices.ContainsKey(typeof(T)) == false)
            {
                throw new Exception($"Service type of {typeof(T).Name} not found.");
            }

            return (T)_registeredServices[typeof(T)].Service;
        }

        private static ContextContainerBase GetDependency(Type dependencyType)
        {
            if (_registeredServices.TryGetValue(dependencyType, out var result))
            {
                return result;
            }

            if (_sceneObjects.TryGetValue(dependencyType, out var sceneObject))
            {
                return sceneObject;
            }

            Log.Error($"Dependency type of '{dependencyType}' not found.");
            return null;
        }

        public static void InitScene()
        {
            _sceneObjects.Clear();
        }

        public static void RegisterSceneObject(Object obj)
        {
            //TODO implement
            var sceneContainer = new SceneContainer(obj.GetType());
            sceneContainer.Instance = obj;
            _sceneObjects.AddOrUpdate(obj.GetType(), sceneContainer);
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

        public static void InitServices()
        {
            foreach (var (_, container) in _registeredServices)
            {
                if (container.Service is IConstruct constructor)
                {
                    constructor.Construct();
                }

                if (container.Service is IConstructInstaller constructInstaller)
                {
                    constructInstaller.Construct(container.ConstructInstaller);
                }
            }
        }
    }
}