using System;
using Main.Services;
using sm_application.Scripts.Main.Wrappers;

namespace Main.Contexts
{
    public class ServiceContainer : ContextContainerBase
    {
        public IService Service;
        public bool IsInitialized;

        public ServiceContainer(Type bindType, ContextScope scope = ContextScope.App) : base(bindType, scope)
        {
        }

        public void Initialize()
        {
            if (Service is IConstruct)
            {
                (Service as IConstruct).Construct();
            }
        }

        public enum ContextScope
        {
            App,
            Scene
        }
    }
}