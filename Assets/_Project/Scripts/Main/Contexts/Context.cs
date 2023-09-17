using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Main.Contexts.DI;
using Main.Contexts.Installers;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;
using UnityEngine;

namespace Main.Contexts
{
    public partial class Context
    {
        private static ProjectContextInstaller _projectContextInstaller;
        private static readonly Dictionary<Type, ContextContainer> _containers = new();
        // private static readonly ConditionalWeakTable<Type, ContextContainer> _sceneObjects = new();
        private static Transform _contextHierarchy;
        private static ContextHandler _contextHandler;

        public static Transform ContextHierarchy => _contextHierarchy;
        public static ProjectContextInstaller ProjectContextInstaller => _projectContextInstaller;

        public static void Init(ProjectContextInstaller projectContextInstaller, Transform contextHierarchy, Transform servicesHierarchy)
        {
            _projectContextInstaller = projectContextInstaller;
            _contextHierarchy = contextHierarchy;
            _contextHandler = new ContextHandler(_containers);
        }

        public static ContextContainer Bind(Type type, ContextScope scope = ContextScope.App)
        {
            if (_containers.ContainsKey(type))
            {
                Log.Error($"Service type of '{type.Name}' registered already");
                return null;
            }

            var contextContainer = new ContextContainer(type, _contextHandler);
            _containers.Add(type, contextContainer);

            return contextContainer;
        }

        public static ContextContainer Bind<T>()
        {
            return Bind(typeof(T));
        }

        public static ContextContainer Bind<T>(GameObject prefab = null, ContextScope scope = ContextScope.App) 
        {
            return Bind(typeof(T), scope);
        }

        public static ContextContainer Bind<T>(MonoBehaviour prefab = null, ContextScope scope = ContextScope.App) where T : IService
        {
            return Bind(typeof(T), scope);
        }

        private static void SetDependencies(ContextContainer container)
        {
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
                    }
                }
            }
        }

        public static T Resolve<T>()
        {
            if (_containers.ContainsKey(typeof(T)) == false)
            {
                throw new Exception($"Dependency type of {typeof(T).Name} not found.");
            }

            return (T)_containers[typeof(T)].Instance;
        }

        private static ContextContainer GetDependency(Type dependencyType)
        {
            if (_containers.TryGetValue(dependencyType, out var result))
            {
                return result;
            }

            Log.Error($"Dependency type of '{dependencyType}' not found.");
            return null;
        }

        public static void DisposeSceneContext()
        {
            foreach (var (key, value) in _containers)
            {
                if (value.Scope == ContextScope.Scene)
                {
                    if (value.Instance is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    
                    _containers.Remove(key);
                }
            }
        }

        public static void Dispose()
        {
            _projectContextInstaller = null;
            
            foreach (var type in _containers.Keys.ToArray())
            {
                if (_containers[type].Instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _containers[type] = null;
            }

            _containers.Clear();
        }

        public static void InitDependencies()
        {
            foreach (var (_, container) in _containers)
            {
                if (container.IsInitialized) continue;

                SetDependencies(container);
                
                container.Initialize();
            }
        }
    }
    
    public class ContextHandler
    {
        private Dictionary<Type, ContextContainer> _containers;
            
        public ContextHandler(Dictionary<Type, ContextContainer> containers)
        {
            _containers = containers;
        }
            
        public void Rebind(Type oldType, Type newType)
        {
            var container = _containers[oldType];
            _containers.Remove(oldType);
            _containers.Add(newType, container);
        }
    }
}