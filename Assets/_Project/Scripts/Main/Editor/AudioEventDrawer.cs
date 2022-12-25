using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Main.Audio.Editor
{
    [CustomEditor(typeof(AudioEvent), true)]
    public class AudioEventDrawer : UnityEditor.Editor {
        [SerializeField]
        private AudioSource audioSource;

        public void OnEnable() {
            audioSource = EditorUtility
                .CreateGameObjectWithHideFlags("Audio Source", HideFlags.HideAndDontSave, typeof(AudioSource))
                .GetComponent<AudioSource>();
        }

        public void OnDisable() {
            DestroyImmediate(audioSource);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUI.BeginDisabledGroup(false);
            if (GUILayout.Button("Preview")) {
                ((AudioEvent) target).Play(audioSource);
            }
            EditorGUI.EndDisabledGroup();
        }

    }
}