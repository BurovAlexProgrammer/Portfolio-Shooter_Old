using System;
using System.Collections.Generic;

namespace Main.Contexts
{
    public abstract class ContextContainerBase
    {
        public Type BindType;
        public Type SourceType;
        public ServiceContainer.Scope Scope = ServiceContainer.Scope.App;
        public List<Type> Dependencies = new();
        public object Instance;

        public ContextContainerBase(Type bindType, ServiceContainer.Scope scope = ServiceContainer.Scope.App)
        {
            Scope = scope;
            BindType = bindType;
            SourceType = bindType;
        }
    }
}