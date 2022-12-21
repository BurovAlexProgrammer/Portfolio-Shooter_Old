using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.Main.Editor
{
    [CustomPropertyDrawer(typeof(ScenePicker))]
    public class ScenePickerDrawer : PropertyDrawer
    {
        SceneAsset _currentScene;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return base.CreatePropertyGUI(property);
            // var container = new VisualElement();
            // var sceneField = new PropertyField(property.FindPropertyRelative("scenePath"));
            //
            // container.Add(sceneField);
            //
            // return container;
        }

        //TODO SceneAsset to string - because cannot build UnityEditor.SceneAsset
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var scenePathProperty = property.FindPropertyRelative("scenePath"); 
            EditorGUI.BeginProperty(position, label, scenePathProperty);
            
             position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
             EditorGUI.PropertyField(position, scenePathProperty, GUIContent.none);
            
            EditorGUI.EndProperty();

           // GUI.changed = false;
           //  var newScene = EditorGUILayout.ObjectField("scene", _currentScene, typeof(SceneAsset), false) as SceneAsset;
           //  
           //  if (GUI.changed)
           //  {
           //      var newPath = AssetDatabase.GetAssetPath(newScene);
           //      var scenePathProperty = property.FindPropertyRelative("scenePath");
           //      scenePathProperty.stringValue = newPath;
           //  }
        }

        // public void ()
        // {
            // var picker = target as ScenePicker;
            // var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.scenePath);
            //
            // serializedObject.Update();
            //
            // EditorGUI.BeginChangeCheck();
            // var newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;
            //
            // if (EditorGUI.EndChangeCheck())
            // {
            //     var newPath = AssetDatabase.GetAssetPath(newScene);
            //     var scenePathProperty = serializedObject.FindProperty("scenePath");
            //     scenePathProperty.stringValue = newPath;
            // }
            // serializedObject.ApplyModifiedProperties();
        // }
    }
}
