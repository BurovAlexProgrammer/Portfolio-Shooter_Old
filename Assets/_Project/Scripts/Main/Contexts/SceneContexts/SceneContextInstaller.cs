using System;
using UnityEngine;

namespace Main.Contexts.Installers
{
    public abstract class SceneContextInstaller : MonoBehaviour
    {
        public void Initialize()
        {
            InstallBindings();
            OnBindingsInstalled();
            Context.InitDependencies();
            OnInitialized();
        }

        private void OnDestroy()
        {
            Context.DisposeSceneContext();
        }

        protected virtual void InstallBindings()
        {
            throw new Exception($"{this.GetType().Name} not implement InstallBindings");
        }

        protected virtual void OnBindingsInstalled()
        {
        }

        protected virtual void OnInitialized()
        {
        }
    }
}