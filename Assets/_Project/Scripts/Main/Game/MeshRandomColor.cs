using UnityEngine;

namespace _Project.Scripts.Main.Game
{
    [ExecuteAlways]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshRandomColor : MonoBehaviour
    {
        [SerializeField] private Color _colorInEditor = Color.white;

        void Start()
        {
            OnChangeColorInEditor();
        }

        private void OnValidate()
        {
            OnChangeColorInEditor();
        }

        void OnChangeColorInEditor()
        {
            if (Application.isPlaying)
            {
                GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
            else
            {
                var sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;
                if (sharedMaterial != null)
                    GetComponent<MeshRenderer>().sharedMaterial.color = _colorInEditor;
            }
        }
    }
}