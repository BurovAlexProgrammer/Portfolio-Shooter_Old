using System;
using System.Collections.Generic;
using Main.Extension;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Contexts
{
    public sealed class ContextContainer
    {
        private List<Type> _dependencies = new();
        private Type _sourceType;
        private Type _bindType;
        private ContextScope _scope;
        private object _instance;
        private ContextHandler _contextHandler;
        private bool _isInitialized;
        
        public bool IsInitialized => _isInitialized;
        public List<Type> Dependencies => _dependencies;
        public Type SourceType => _sourceType;
        public Type BindType => _bindType;
        public ContextScope Scope => _scope;
        public object Instance => _instance;

        public ContextContainer(Type bindType, ContextHandler contextHandler, ContextScope scope = ContextScope.App)
        {
            _contextHandler = contextHandler;
            _scope = scope;
            _bindType = bindType;
            _sourceType = bindType;
        }
        
        public void Initialize()
        {
            if (Instance is IConstruct)
            {
                (Instance as IConstruct).Construct();
            }

            _isInitialized = true;
        }

        public ContextContainer As<T>()
        {
            return As(typeof(T));
        }

        public ContextContainer As(Type type)
        {
            _contextHandler.Rebind(_bindType, type);
            _bindType = type;
            return this;
        }
        
        public ContextContainer FromInstance(GameObject gameObject)
        {
            _instance = gameObject;
            return this;
        }

        public ContextContainer FromInstance(MonoBehaviour monoBehaviour)
        {
            _instance = monoBehaviour;
            return this;
        }

        public ContextContainer FromNew()
        {
            if (SourceType.GetType().IsSubclassOf(typeof(MonoBehaviour)))
            {
                Log.Error($"Cannot bind source type '{SourceType.Name}' FromNew. Current type is MonoBehaviour. Use FromNewPrefab() instead.");
                return this;
            }
            
            _instance = Activator.CreateInstance(SourceType);
            return this;
        }
        
        public ContextContainer FromNewPrefab(GameObject prefab)
        {
            if (SourceType.IsSubclassOf(typeof(MonoBehaviour)) == false)
            {
                Log.Error($"Cannot bind source type '{SourceType.Name}' FromNewPrefab. Current type is not MonoBehaviour. Use FromNew() instead.");
                return this;
            }
            
            _instance = Object.Instantiate(prefab);
            (_instance as GameObject).CleanName();
            
            if (_scope == ContextScope.App)
            {
                (_instance as GameObject).transform.SetParent(Context.ContextHierarchy);
            }
            
            return this;
        }

        public ContextContainer FromNewPrefab(MonoBehaviour prefab)
        {
            FromNewPrefab(prefab.gameObject);
            var component = (_instance as GameObject).GetComponent(prefab.GetType());
            _instance = component;
            return this;
        }
    }
}