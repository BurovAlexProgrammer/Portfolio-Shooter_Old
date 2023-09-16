using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Main.Contexts.DI;
using Main.Contexts.Installers;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;
using UnityEngine;
using Object = System.Object;

namespace Main.Contexts
{
    public class Context
    {
        public static ProjectContextInstaller ProjectContextInstaller;
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

        public static ServiceContainerBuilder BindService(Type type, GameObject prefab = null, ServiceContainer.ContextScope scope = ServiceContainer.ContextScope.App)
        {
            if (_registeredServices.ContainsKey(type))
            {
                Log.Error($"Service type of '{type.Name}' registered already");
                return null;
            }

            if (type.IsSubclassOf(typeof(MonoBehaviour)) && prefab == null)
            {
                Log.Warn($"Service type of '{type.Name}' is MonoBehaviour. Add prefab instance to instantiate.");
            }

            var serviceContainerBuilder = new ServiceContainerBuilder(type);
            var serviceContainer = serviceContainerBuilder.ServiceContainer;
            serviceContainer.BindType = type;
            serviceContainer.SourceType = type;

            if (type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var gameObject = UnityEngine.Object.Instantiate(prefab, _servicesHierarchy);
                gameObject.name = serviceContainer.SourceType.Name;
                var service = gameObject.GetComponent(serviceContainer.BindType);
                serviceContainer.Instance = service;
                serviceContainer.Service = service as IService;
            }
            else
            {
                serviceContainer.Service = Activator.CreateInstance(type) as IService;
                serviceContainer.Instance = serviceContainer.Service;
            }

            _registeredServices.Add(type, serviceContainer);

            return serviceContainerBuilder;
        }

        public static ServiceContainerBuilder BindService<T>() where T : IService
        {
            return BindService(typeof(T));
        }

        public static ServiceContainerBuilder BindService<T>(GameObject prefab = null, ServiceContainer.ContextScope scope = ServiceContainer.ContextScope.App) where T : IService
        {
            return BindService(typeof(T), prefab, scope);
        }

        public static ServiceContainerBuilder BindService<T>(MonoBehaviour prefab = null, ServiceContainer.ContextScope scope = ServiceContainer.ContextScope.App) where T : IService
        {
            return BindService(typeof(T), prefab.gameObject, scope);
        }

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
            ProjectContextInstaller = null;
            
            foreach (var type in _registeredServices.Keys.ToArray())
            {
                if (_registeredServices[type] is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _registeredServices[type] = null;
            }

            _registeredServices.Clear();
            _sceneObjects.Clear();
        }

        public static void InitServices()
        {
            foreach (var (_, container) in _registeredServices)
            {
                if (container.IsInitialized) continue;

                SetDependencies(container);

                if (container.Service is IConstruct)
                {
                    container.Initialize();
                }

                container.IsInitialized = true;
            }
        }

        public class ServiceContainerBuilder
        {
            public ServiceContainer ServiceContainer;

            public ServiceContainerBuilder(ServiceContainer serviceContainer)
            {
                ServiceContainer = serviceContainer;
            }

            public ServiceContainerBuilder(Type type)
            {
                var serviceContainer = new ServiceContainer(type);
                ServiceContainer = serviceContainer;
            }

            public ServiceContainerBuilder As<T>()
            {
                _registeredServices.Remove(ServiceContainer.BindType);
                ServiceContainer.BindType = typeof(T);
                _registeredServices.Add(typeof(T), ServiceContainer);
                return this;
            }

            public ServiceContainerBuilder WithInstaller()
            {
                //TODO find installer
                if (ServiceContainer.Service.GetType().IsSubclassOf(typeof(MonoBehaviour)))
                {
                }

                return this;
            }
        }
    }
}