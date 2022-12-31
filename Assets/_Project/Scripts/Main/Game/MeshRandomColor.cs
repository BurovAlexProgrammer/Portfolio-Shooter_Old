using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Main.Game
{
    [ExecuteAlways]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshRandomColor : MonoBehaviour
    {
        [SerializeField] private Color _colorInEditor = Color.white;

        void Start()
        {
            ChangeColorInPlayMode();
        }

        private void OnValidate()
        {
            if (Application.isPlaying) return;

            ChangeColorInEditMode();
        }

        void ChangeColorInEditMode()
        {
            var sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;

            sharedMaterial.color = _colorInEditor;
        }

        void ChangeColorInPlayMode()
        {
            if (Application.isPlaying == false) return;
            
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
    }
}