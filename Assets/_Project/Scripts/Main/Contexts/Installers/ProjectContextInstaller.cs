using System;
using UnityEngine;

#if UNITY_EDITOR
using smApplication.Scripts.Extension;
#endif

namespace Main.Contexts.Installers
{
    public abstract class ProjectContextInstaller : MonoBehaviour
    {
        private Transform ContextHierarchy;
        private Transform ServicesHierarchy;

        private void Awake()
        {
            ContextHierarchy = transform;
            DontDestroyOnLoad(gameObject);
            ServicesHierarchy = new GameObject() { name = "Services" }.transform;
            ServicesHierarchy.SetParent(ContextHierarchy);
            Context.Init(this, ContextHierarchy, ServicesHierarchy);
            InstallBindings();
            OnBindingsInstalled();
            Context.InitDependencies();
            OnServicesInitialized();
        }

        private void Start()
        {
#if UNITY_EDITOR
            UnityEditorUtility.ExpandScene(ContextHierarchy.gameObject.scene);
            UnityEditorUtility.ExpandHierarchyItem(ContextHierarchy);
            UnityEditorUtility.ExpandHierarchyItem(ServicesHierarchy);
            UnityEditorUtility.ExpandHierarchyItem(ServicesHierarchy);
#endif
        }

        private void OnApplicationQuit()
        {
            Context.Dispose();
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