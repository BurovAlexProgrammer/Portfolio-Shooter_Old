using System;
using System.Collections.Generic;

namespace _Project.Scripts.Main.AppServices.Base
{
    public sealed class ServiceLocator<T> : IServiceLocator<T> where T: class
    {
        private readonly Dictionary<Type, T> _serviceMap;
        public Dictionary<Type, T> ServiceMap => _serviceMap;

        public ServiceLocator()
        {
            _serviceMap = new Dictionary<Type, T>();
        }
        
        public T2 Register<T2>(T2 newService) where T2 : T
        {
            var serviceType = newService.GetType();
                
            if (ServiceMap.ContainsKey(serviceType))
                throw new Exception($"Service {serviceType.Name} already registered.");
                
            ServiceMap.Add(serviceType, newService);
            return newService;
        }

        public void Unregister<T2>(T2 service) where T2 : T
        {
            var serviceType = service.GetType();
                
            if (!ServiceMap.ContainsKey(serviceType))
                throw new Exception($"Service {serviceType.Name} not found in ServiceLocator to Unregister.");
                
            ServiceMap.Remove(serviceType);
        }

        public void Clear()
        {
            ServiceMap.Clear();
        }

        public T2 Get<T2>() where T2 : T
        {
            var serviceType = typeof(T2);
                
            if (!ServiceMap.TryGetValue(serviceType, out var result))
                throw new Exception($"Service {serviceType.Name} not found in ServiceLocator to Get.");
                
            return (T2)result;
        }
    }
}