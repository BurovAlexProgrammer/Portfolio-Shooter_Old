#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace smApplication.Scripts.Extension
{
    public class UnityEditorUtility
    {
        public static void CollapseHierarchyItem(Object gameObject)
        {
            SetCollapseHierarchyItem(gameObject);
        }
        public static void ExpandHierarchyItem(Object gameObject)
        {
            SetCollapseHierarchyItem(gameObject, false);
        }

        public static void ExpandScene(Scene scene)
        {
            var hierarchy = GetFocusedWindow("Hierarchy");

            if (hierarchy == null)
            {
                Debug.LogWarning("Hierarchy window ");
                return;
            }

            var list = new List<GameObject>();
            scene.GetRootGameObjects(list);

            Selection.activeObject = list[0];
            
            var key = new Event { keyCode = KeyCode.UpArrow, type = EventType.KeyDown };
            hierarchy.SendEvent(key);
            
            key = new Event { keyCode =  KeyCode.RightArrow , type = EventType.KeyDown };
            hierarchy.SendEvent(key);
        }
        
        private static void SetCollapseHierarchyItem(Object gameObject, bool collapse = true)
        {
            var hierarchy = GetFocusedWindow("Hierarchy");

            if (hierarchy == null)
            {
                Debug.LogWarning("Hierarchy window ");
                return;
            }
            
            Selection.activeObject = gameObject;
            var key = new Event { keyCode = collapse ? KeyCode.LeftArrow : KeyCode.RightArrow, type = EventType.KeyDown };
            hierarchy.SendEvent(key);
        }

        public static EditorWindow GetFocusedWindow(string window)
        {
            FocusOnWindow(window);
            return EditorWindow.focusedWindow;
        }
        
        public static void FocusOnWindow(string window)
        {
            EditorApplication.ExecuteMenuItem("Window/General/" + window);
        }
    }
}
#endif
