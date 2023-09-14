using smApplication.Scripts.Extension;
using UnityEngine;

namespace Main.Contexts
{
    public class ContextComponent : MonoBehaviour
    {
        private Transform ContextHierarchy;
        private Transform ServicesHierarchy;
        
        private void Awake()
        {
            ContextHierarchy = transform;
            DontDestroyOnLoad(gameObject);
            ServicesHierarchy = new GameObject() {name = "Services"}.transform;
            ServicesHierarchy.SetParent(ContextHierarchy);
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