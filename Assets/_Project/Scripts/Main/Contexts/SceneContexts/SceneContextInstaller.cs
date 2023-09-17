using System;
using smApplication.Scripts.Extension;
using UnityEngine;

namespace Main.Contexts.Installers
{
    public abstract class SceneContextInstaller : MonoBehaviour
    {
        private void Awake()
        {
            InstallBindings();
            OnBindingsInstalled();
            Context.InitDependencies();
            OnServicesInitialized();
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

        protected virtual void OnServicesInitialized()
        {
        }
    }
}