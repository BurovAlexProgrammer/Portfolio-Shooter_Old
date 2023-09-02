using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNamespace
{
    public class IoC
    {
        private readonly IDictionary<Type, RegisteredObject> _registeredObjects = new Dictionary<Type, RegisteredObject>();

        public void Register<TType>() where TType : class
        {
            Register<TType, TType>(false, null);
        }

        public void Register<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(false, null);
        }

        public void RegisterSingleton<TType>() where TType : class
        {
            RegisterSingleton<TType, TType>();
        }

        public void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, null);
        }

        public void RegisterInstance<TType>(TType instance) where TType : class
        {
            RegisterInstance<TType, TType>(instance);
        }

        public void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, instance);
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
        }

        public object Resolve(Type type)
        {
            return ResolveObject(type);
        }

        private void Register<TType, TConcrete>(bool isSingleton, TConcrete instance)
        {
            Type type = typeof(TType);
            if (_registeredObjects.ContainsKey(type))
                _registeredObjects.Remove(type);
            _registeredObjects.Add(type, new RegisteredObject(typeof(TConcrete), isSingleton, instance));
        }

        private object ResolveObject(Type type)
        {
            var registeredObject = _registeredObjects[type];
            if (registeredObject == null)
            {
                throw new ArgumentOutOfRangeException(string.Format("The type {0} has not been registered", type.Name));
            }

            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            object instance = registeredObject.SingletonInstance;
            if (instance == null)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                instance = registeredObject.CreateInstance(parameters.ToArray());
            }

            return instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            return constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType));
        }

        private class RegisteredObject
        {
            private readonly bool _isSinglton;

            public RegisteredObject(Type concreteType, bool isSingleton, object instance)
            {
                _isSinglton = isSingleton;
                ConcreteType = concreteType;
                SingletonInstance = instance;
            }

            public Type ConcreteType { get; private set; }
            public object SingletonInstance { get; private set; }

            public object CreateInstance(params object[] args)
            {
                object instance = Activator.CreateInstance(ConcreteType, args);
                if (_isSinglton)
                    SingletonInstance = instance;
                return instance;
            }
        }
    }
}