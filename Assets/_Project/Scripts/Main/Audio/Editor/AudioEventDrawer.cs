using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioEvent), true)]
public class AudioEventDrawer : Editor {
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