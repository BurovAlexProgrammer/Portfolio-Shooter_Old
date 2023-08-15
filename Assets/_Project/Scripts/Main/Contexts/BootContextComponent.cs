using smApplication.Scripts.Extension;
using UnityEngine;

namespace Main.Contexts
{
    public class BootContextComponent : MonoBehaviour
    {
        private Transform ContextHierarchy;
        private Transform ServicesHierarchy;
        
        private void Awake()
        {
            ContextHierarchy = new GameObject() {name = "Context"}.transform;
            DontDestroyOnLoad(ContextHierarchy.gameObject);
            ServicesHierarchy = new GameObject() {name = "Services"}.transform;
            ServicesHierarchy.SetParent(ContextHierarchy);
            DontDestroyOnLoad(ServicesHierarchy.gameObject);
            Context.Init(ContextHierarchy, ServicesHierarchy);
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
    }
}