using System;

namespace Main.Contexts
{
    public class SceneContainer : ContextContainerBase
    {
        public SceneContainer(Type bindType, ServiceContainer.Scope scope = ServiceContainer.Scope.App) : base(bindType, scope)
        {
        }
    }
}