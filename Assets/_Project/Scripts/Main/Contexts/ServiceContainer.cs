using System;
using System.Collections.Generic;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;

namespace Main.Contexts
{
    public class ServiceContainer : ContextContainerBase
    {
        public IService Service;
        public IServiceInstaller ConstructInstaller;
        public bool IsInitialized;

        public Queue<Type> UnresolvedDependencies = new();

        public ServiceContainer(Type bindType, Scope scope = ServiceContainer.Scope.App) : base(bindType, scope)
        {
        }
        
        public void Initialize()
        {
            if (Service is IConstruct)
            {
                (Service as IConstruct).Construct();
            }

            if (Service is IConstructInstaller)
            {
                Log.Error($"Service {BindType.Name} has Construct. Use Services.RegisterService(IServiceInstaller installer) instead");
            }
        }
        
        public void Initialize(IServiceInstaller installer)
        {
            if (Service is not IConstructInstaller)
            {
                throw new Exception($"Service {BindType.Name} doesn't have Construct. Use Services.RegisterService() instead");
            }

            (Service as IConstructInstaller).Construct(installer);
        }
        
        public enum Scope
        {
            App,
            Scene
        }
    }
}