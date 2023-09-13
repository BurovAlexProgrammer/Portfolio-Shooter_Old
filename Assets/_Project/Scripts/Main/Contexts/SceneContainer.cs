using System;

namespace Main.Contexts
{
    public class SceneContainer : ContextContainerBase
    {
        public SceneContainer(Type bindType, ServiceContainer.ContextScope scope = ServiceContainer.ContextScope.App) : base(bindType, scope)
        {
        }
    }
}