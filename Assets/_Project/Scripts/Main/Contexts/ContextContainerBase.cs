using System;
using System.Collections.Generic;

namespace Main.Contexts
{
    public abstract class ContextContainerBase
    {
        public Type BindType;
        public Type SourceType;
        public ServiceContainer.ContextScope Scope = ServiceContainer.ContextScope.App;
        public List<Type> Dependencies = new();
        public object Instance;

        public ContextContainerBase(Type bindType, ServiceContainer.ContextScope scope = ServiceContainer.ContextScope.App)
        {
            Scope = scope;
            BindType = bindType;
            SourceType = bindType;
        }
    }
}